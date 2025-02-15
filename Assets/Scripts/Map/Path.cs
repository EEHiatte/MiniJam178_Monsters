using System;
using UnityEngine;
using UnityEngine.Splines;

/// <summary>
/// Class that will contain the spline that enemies follow.
/// </summary>
public class Path : MonoBehaviour
{
    [SerializeField]
    private SplineContainer splineContainer; // Maybe change it to something dynamic instead of serialized.

    [SerializeField] private Transform bulgeTransform;

    [SerializeField] private MeshRenderer pathRenderer;

    [SerializeField] private MeshRenderer backgroundPathRenderer;
    
    public SplineContainer SplineContainer => splineContainer;

    private void Update()
    {
        pathRenderer.material.SetVector("_BulgePosition", bulgeTransform.localPosition);
        backgroundPathRenderer.material.SetVector("_BulgePosition", bulgeTransform.localPosition);
    }
}
