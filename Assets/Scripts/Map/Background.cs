using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform bulgeTransformA;
    
    [SerializeField] private Transform bulgeTransformB;
    
    [SerializeField] private Transform bulgeTransformC;

    [SerializeField] private MeshRenderer backgroundRenderer;
    
    private void Update()
    {
        backgroundRenderer.material.SetVector("_ABulgePosition", bulgeTransformA.localPosition);
        backgroundRenderer.material.SetVector("_BBulgePosition", bulgeTransformB.localPosition);
        backgroundRenderer.material.SetVector("_CBulgePosition", bulgeTransformC.localPosition);
    }
}
