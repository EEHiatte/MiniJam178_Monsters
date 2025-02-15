using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerPlacement towerPlacement;
    [SerializeField] private TowerRange towerRange;
    [SerializeField] private float towerFireRate;
    
    [SerializeField] private int towerCost;
    
    // Placement
    private bool _towerBeingPlaced = false;
    public event EventHandler TowerPlacementResolved;
    
    public void PlaceTower()
    {
        _towerBeingPlaced = true;
        FollowMouse();
    }
    
    private void Update()
    {
        towerPlacement.gameObject.SetActive(_towerBeingPlaced);
        this.tag = _towerBeingPlaced ? "Tower_Placement" : "Tower";
        
        if (!_towerBeingPlaced)
        {
            return;
        }

        towerRange.ShowRangeIndicator(true);
        FollowMouse();
        CheckInput();
    }

    private void FollowMouse()
    {
        var mainCamera = Camera.main;
        var mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
        gameObject.transform.position = mousePosition;
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (towerPlacement.IsValidPlacement)
            {
                _towerBeingPlaced = false;
                towerPlacement.gameObject.SetActive(false);
                towerRange.ShowRangeIndicator(false);
                TowerPlacementResolved?.Invoke(null, null);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            TowerPlacementResolved?.Invoke(null, null);
            Destroy(this.gameObject);
            // TODO notify the game controller to refund?
        }
    }
    
    private void OnMouseEnter()
    {
        if (_towerBeingPlaced)
        {
            return;
        }
        towerRange.ShowRangeIndicator(true);
    }

    private void OnMouseExit()
    {
        if (_towerBeingPlaced)
        {
            return;
        }
        towerRange.ShowRangeIndicator(false);
    }
}
