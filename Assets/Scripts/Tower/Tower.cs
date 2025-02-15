using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerPlacement towerPlacement;
    [SerializeField] protected TowerRange towerRange;
    [SerializeField] protected DamagingTowerBase damagingTowerHandler;
   
    [SerializeField] private float towerFireRate;
    [SerializeField] private float towerDamage;
    
    [SerializeField] private int towerCost;
    
    private TowerState _towerState = TowerState.Disabled;
    public event EventHandler TowerPlacementResolved;

    private TowerActiveHelper _towerActiveHelper = new TowerActiveHelper();
    
    public class TowerActiveHelper
    {
        private float _attackTimer;

        public bool CanAttack => _attackTimer <= 0f;
        
        public void Update(float deltaTime)
        {
            if (_attackTimer > 0f)
                _attackTimer -= deltaTime;
        }

        public void Reset(float attackRate)
        {
            _attackTimer = attackRate;
        }
    }
    
    public void PlaceTower()
    {
        _towerState = TowerState.Placing;
        FollowMouse();
        this.tag = "Tower_Placement";
        towerPlacement.gameObject.SetActive(true);
        
        _towerActiveHelper.Reset(towerFireRate);
    }
    
    private void Update()
    {
        if (_towerState == TowerState.Disabled)
        {
            return;
        }
        
        if (_towerState == TowerState.Placing)
        {
            towerRange.ShowRangeIndicator(true);
            FollowMouse();
            CheckInput();
        }
        else if (_towerState == TowerState.Active)
        {
            _towerActiveHelper.Update(Time.deltaTime);

            if (_towerActiveHelper.CanAttack)
            {
                if (damagingTowerHandler.OnTryDamage(new DamagingTowerBase.DamageInfo(towerRange, towerDamage)))
                {
                    _towerActiveHelper.Reset(towerFireRate);
                }
            }
        }
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
                _towerState = TowerState.Active;
                towerPlacement.gameObject.SetActive(false);
                towerRange.ShowRangeIndicator(false);
                this.tag = "Tower";
                TowerPlacementResolved?.Invoke(null, null);
                // TODO charge the coin balance.
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            TowerPlacementResolved?.Invoke(null, null);
            Destroy(this.gameObject);
        }
    }
    
    private void OnMouseEnter()
    {
        if (_towerState == TowerState.Placing)
        {
            return;
        }
        towerRange.ShowRangeIndicator(true);
    }

    private void OnMouseExit()
    {
        if (_towerState == TowerState.Placing)
        {
            return;
        }
        towerRange.ShowRangeIndicator(false);
    }
}

public enum TowerState
{
    Disabled,
    Placing,
    Active
}
