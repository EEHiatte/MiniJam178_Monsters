using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerPlacement towerPlacement;
    
    // By default the tower isn't placed...
    private bool _towerBeingPlaced = true;
    
    private void Update()
    {
        towerPlacement.gameObject.SetActive(_towerBeingPlaced);
        this.tag = _towerBeingPlaced ? "Tower_Placement" : "Tower";
        
        if (!_towerBeingPlaced)
        {
            return;
        }
        
        var mainCamera = Camera.main;
        var mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
        this.gameObject.transform.position = mousePosition;

        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (towerPlacement.IsValidPlacement)
            {
                _towerBeingPlaced = false;
                towerPlacement.gameObject.SetActive(false);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Destroy(this.gameObject);
            // TODO notify the game controller to refund?
        }
    }
    
    
}
