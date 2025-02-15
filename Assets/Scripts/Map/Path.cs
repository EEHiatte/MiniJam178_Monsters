using UnityEngine;
using UnityEngine.Splines;

/// <summary>
/// Class that will contain the spline that enemies follow.
/// </summary>
public class Path : MonoBehaviour
{
    [SerializeField]
    private SplineContainer splineContainer; // Maybe change it to something dynamic instead of serialized.
    
    public SplineContainer SplineContainer => splineContainer;
}
