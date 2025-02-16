using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Background : MonoBehaviour
{
    [SerializeField] private Path path;
    
    [SerializeField] private MeshRenderer backgroundRenderer;
    
    [SerializeField]
    private List<BackgroundDisplacer> displacers = new List<BackgroundDisplacer>();
    
    [Serializable]
    private class BackgroundDisplacer
    {
        public Transform transform;
        public float radius;
        public float strength;
        public bool Moving { get; set; }
    }
    
    private void Start()
    {
        //for (int i = 0; i < displacers.Count; i++)
        //{
        //    path.AddDisplacementObject(displacers[i].transform, displacers[i].radius, displacers[i].strength);
        //}
    }

    private void OnDestroy()
    {
        for (int i = 0; i < displacers.Count; i++)
        {
            path.RemoveDisplacementObject(displacers[i].transform);
        }
    }

    private void Update()
    {
        //for (int i = 0; i < displacers.Count; i++)
        //{
        //    if(displacers[i].Moving == false)
        //    {
        //        var insideUnitCircle = Random.insideUnitCircle * 10;
        //        transform.position = new Vector3(insideUnitCircle.x, insideUnitCircle.y, 0);
        //        StartCoroutine(MovementRoutine(displacers[i], insideUnitCircle));
        //    }
        //}
    }
    
    private IEnumerator MovementRoutine(BackgroundDisplacer displacer, Vector3 targetPosition)
    {
        displacer.Moving = true;
        while (displacer.transform.localPosition != targetPosition)
        {
            displacer.transform.localPosition = Vector3.MoveTowards(displacer.transform.localPosition, targetPosition, Time.deltaTime);
            yield return null;
        }
        displacer.Moving = false;
    }
}
