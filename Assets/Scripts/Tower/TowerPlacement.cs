using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    private bool _isValidPlacement = true;

    public bool IsValidPlacement => _isValidPlacement;

    private bool DEBUG = false;
    
    private void OnTriggerEnter(Collider other)
    {
        _isValidPlacement = false;

        if (DEBUG)
        {
            Debug.Log($"OnTriggerEnter with {other.gameObject.name}");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        _isValidPlacement = true;
        
        if (DEBUG)
        {
            Debug.Log($"OnTriggerExit with {other.gameObject.name}");
        }
    }
}
