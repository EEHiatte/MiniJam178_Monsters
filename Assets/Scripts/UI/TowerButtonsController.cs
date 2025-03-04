using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowerButtonsController : MonoBehaviour
{
    private Dictionary<TowerButtonType, TowerButton> _towerButtons = new();
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private LevelController levelController;
    [SerializeField] private TooltipTextController tooltipTextController;
    
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
            towerButton.MouseOver += TowerButtonOnMouseOver;
            towerButton.MouseExit += TowerButtonOnMouseExit;
        }
        
        levelController.OnCurrencyUpdate += LevelControllerOnOnCurrencyUpdate;
        LevelControllerOnOnCurrencyUpdate(null, null);
    }

    private void TowerButtonOnMouseOver(object sender, EventArgs e)
    {
        if (sender is TowerButton towerButton)
        {
            tooltipTextController.OnTowerButtonHover(towerButton.TowerType);
        }
    }
    
    private void TowerButtonOnMouseExit(object sender, EventArgs e)
    {
        tooltipTextController.OnTowerButtonLeave();
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
        tooltipTextController.OnTowerPlacement();
        
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        var spawnedTower = Instantiate(towerButton.TowerToSpawn, Vector3.zero, randomRotation);
        spawnedTower.StartPlacement();
        spawnedTower.TowerPlacementResolved += SpawnedTowerResolved;
    }


    private void SpawnedTowerResolved(object sender, EventArgs e)
    {
        if (sender is Tower tower) // sends null as sender if destroyed and not placed
        {
            levelController.TowersBuilt += 1;
            levelController.GoldSpent += tower.TowerCost;
            levelController.PlayerCurrency -= tower.TowerCost;
        }
        tooltipTextController.OnTowerPlacementEnd();
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
    HeatTower,
    SlowTower,
    AcidTower,
}
