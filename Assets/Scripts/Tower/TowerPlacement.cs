using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer placementSprite;
    
    private bool _isValidPlacement = true;

    public bool IsValidPlacement => _isValidPlacement;

    private bool DEBUG = true;

    private void Awake()
    {
        SetColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower_Placement"))
        {
            return;
        }
        
        _isValidPlacement = false;
        SetColor();
        
        if (DEBUG)
        {
            Debug.Log($"OnTriggerEnter with {other.gameObject.name}");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower_Placement"))
        {
            return;
        }
        
        _isValidPlacement = true;
        SetColor();

        if (DEBUG)
        {
            Debug.Log($"OnTriggerExit with {other.gameObject.name}");
        }
    }

    public void DisableVisuals()
    {
        placementSprite.enabled = false;
    }

    private void SetColor()
    {
        placementSprite.color = _isValidPlacement ? Color.green : Color.red;
    }
}
