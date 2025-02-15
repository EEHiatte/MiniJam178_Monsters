using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TowerButtonsController : MonoBehaviour
{
    private Dictionary<TowerButtonType, TowerButton> _towerButtons = new();
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private LevelController levelController;
    
    private void Start()
    {
        foreach (var towerButton in this.GetComponentsInChildren<TowerButton>())
        {
            if(_towerButtons.ContainsKey(towerButton.TowerType))
            {
                throw new Exception($"Trying to add tower button in {nameof(TowerButtonsController)} but this already is in the list.");
            }

            _towerButtons.Add(towerButton.TowerType, towerButton);
            towerButton.Button.onClick.AddListener(() => TowerButtonClicked(towerButton));
        }
        
        levelController.OnCurrencyUpdate += LevelControllerOnOnCurrencyUpdate;
        LevelControllerOnOnCurrencyUpdate(null, null);
    }

    private void LevelControllerOnOnCurrencyUpdate(object sender, EventArgs e)
    {
        foreach (var towerButton in _towerButtons.Values)
        {
            towerButton.OnCurrencyUpdate(levelController.PlayerCurrency);
        }
    }

    private void TowerButtonClicked(TowerButton towerButton)
    {
        SetButtonsVisibility(false);
        var spawnedTower = Instantiate(towerButton.TowerToSpawn, Vector3.zero, quaternion.identity);
        spawnedTower.StartPlacement();
        spawnedTower.TowerPlacementResolved += SpawnedTowerResolved;
    }


    private void SpawnedTowerResolved(object sender, EventArgs e)
    {
        if (sender is Tower tower) // sends null as sender if destroyed and not placed
        {
            levelController.PlayerCurrency -= tower.TowerCost;
        }
        
        SetButtonsVisibility(true);
    }

    private void SetButtonsVisibility(bool visible)
    {
        canvasGroup.alpha = visible ? 1 : 0;
    }
}

public enum TowerButtonType
{
    None,
    WhiteBloodCell,
    Tower2Temp,
}
