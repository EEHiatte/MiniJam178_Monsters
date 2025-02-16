using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Splines;

/// <summary>
/// Class that will contain the spline that enemies follow.
/// </summary>
public class Path : MonoBehaviour
{
    // A simple struct matching the one in the compute shader.
    [StructLayout(LayoutKind.Sequential)]
    public struct DisplacementPoint
    {
        public Vector3 position;
        public float radius;
        public float strength;
    }
    
    [SerializeField]
    private SplineContainer splineContainer; // Maybe change it to something dynamic instead of serialized.

    [SerializeField] public Transform bulgeTransform;
    
    [SerializeField] private MeshFilter pathMeshFilter;

    [SerializeField] private MeshFilter backgroundPathMeshFilter;

    [SerializeField] private ComputeShader pathDisplacementShader;
    
    [SerializeField] private int maxDisplacementPoints = 1024;


    /// <summary>
    /// Lists of all Levels spline assets
    /// </summary>
    [SerializeField] public List<SplineContainer> levelSplineContainers;

    [SerializeField] public List<Transform> levelBulgeTransforms;

    [SerializeField] public List<MeshFilter> levelPathMeshFilters;

    [SerializeField] public List<MeshFilter> levelBackgroundPathMeshFilters;


    private List<Transform> displacementTransforms = new List<Transform>();
    
    private List<DisplacementPoint> displacementPoints = new List<DisplacementPoint>();
    
    private Mesh pathMesh;
    private Mesh backgroundPathMesh;
    
    private ComputeBuffer displacementBuffer;
    private ComputeBuffer vertexBuffer;
    private ComputeBuffer backgroundVertexBuffer;

    private Vector3[] pathVertices;
    private Vector3[] backgroundPathVertices;
    
    public SplineContainer SplineContainer => splineContainer;

    public void AddDisplacementObject(Transform displacer, float radius, float strength)
    {
        displacementTransforms.Add(displacer);
        displacementPoints.Add(new DisplacementPoint
        {
            position = displacer.localPosition,
            radius = radius,
            strength = strength
        });
    }
    
    public void RemoveDisplacementObject(Transform displacer)
    {
        int index = displacementTransforms.IndexOf(displacer);
        if (index != -1)
        {
            displacementTransforms.RemoveAt(index);
            displacementPoints.RemoveAt(index);
        }
    }
    
    private void Start()
    {
        pathMesh = pathMeshFilter.mesh;
        pathVertices = pathMesh.vertices;
        backgroundPathMesh = backgroundPathMeshFilter.mesh;
        backgroundPathVertices = backgroundPathMesh.vertices;
        
        vertexBuffer = new ComputeBuffer(pathVertices.Length, Marshal.SizeOf(typeof(Vector3)));
        backgroundVertexBuffer = new ComputeBuffer(backgroundPathVertices.Length, Marshal.SizeOf(typeof(Vector3)));
        vertexBuffer.SetData(pathVertices);
        backgroundVertexBuffer.SetData(backgroundPathVertices);
        
        displacementBuffer = new ComputeBuffer(maxDisplacementPoints, Marshal.SizeOf(typeof(DisplacementPoint)));
        
        AddDisplacementObject(bulgeTransform, 2, 0.25f);
    }
    
    private void Update()
    {
        for (int i = 0; i < displacementTransforms.Count; i++)
        {
            displacementPoints[i] = new DisplacementPoint
            {
                position = displacementTransforms[i].localPosition,
                radius = displacementPoints[i].radius,
                strength = displacementPoints[i].strength
            };
        }
        
        vertexBuffer.SetData(pathVertices);
        backgroundVertexBuffer.SetData(backgroundPathVertices);
        displacementBuffer.SetData(displacementPoints);
        
        int kernel = pathDisplacementShader.FindKernel("CSMain");
        
        pathDisplacementShader.SetBuffer(kernel, "_Vertices", vertexBuffer);
        pathDisplacementShader.SetBuffer(kernel, "_BackgroundVertices", backgroundVertexBuffer);
        pathDisplacementShader.SetBuffer(kernel, "_DisplacementPoints", displacementBuffer);
        pathDisplacementShader.SetFloat("_DisplacementPointsCount", displacementPoints.Count);

        int threadGroups = Mathf.CeilToInt(pathVertices.Length / 64.0f);
        pathDisplacementShader.Dispatch(kernel, threadGroups, 1, 1);
        
        Vector3[] modifiedPathVertices = new Vector3[pathVertices.Length];
        vertexBuffer.GetData(modifiedPathVertices);
        Vector3[] modifiedBackgroundPathVertices = new Vector3[backgroundPathVertices.Length];
        backgroundVertexBuffer.GetData(modifiedBackgroundPathVertices);
        
        pathMesh.vertices = modifiedPathVertices;
        
        backgroundPathMesh.vertices = modifiedBackgroundPathVertices;
    }
    
    private void OnDestroy()
    {
        if (vertexBuffer != null) vertexBuffer.Release();
        if (backgroundVertexBuffer != null) backgroundVertexBuffer.Release();
        if (displacementBuffer != null) displacementBuffer.Release();
    }
}
