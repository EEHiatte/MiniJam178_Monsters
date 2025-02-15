using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    private bool _isValidPlacement = true;

    public bool IsValidPlacement => _isValidPlacement;

    private void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _isValidPlacement = false;
        Debug.Log($"OnTriggerEnter with {other.gameObject.name}");
    }
    
    private void OnTriggerExit(Collider other)
    {
        _isValidPlacement = true;
        Debug.Log($"OnTriggerExit with {other.gameObject.name}");
    }
}
