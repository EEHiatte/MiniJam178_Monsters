using TMPro;
using UnityEngine;

public class TooltipTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tooltipText;

    private string TowerPlacementText = "Left-Click to place / Right-Click to cancel";
    private bool placingTowerFlag = false;
    private TowerButtonType hoveredTowerButton = TowerButtonType.None;
    
    private void Awake()
    {
        tooltipText.text = string.Empty;
        tooltipText.enabled = false;
    }

    public void OnTowerButtonHover(TowerButtonType towerType)
    {
        hoveredTowerButton = towerType;
        CheckForText();
    }

    public void OnTowerButtonLeave()
    {
        hoveredTowerButton = TowerButtonType.None;
        CheckForText();
    }

    public void OnTowerPlacement()
    {
        placingTowerFlag = true;
        CheckForText();
    }

    public void OnTowerPlacementEnd()
    {
        placingTowerFlag = false;
        CheckForText();
    }

    private void CheckForText()
    {
        tooltipText.enabled = placingTowerFlag || hoveredTowerButton != TowerButtonType.None; 

        if (placingTowerFlag == true)
        {
            tooltipText.text = TowerPlacementText;
        }
        else if (hoveredTowerButton != TowerButtonType.None)
        {
            tooltipText.text = GetToolTipForTower(hoveredTowerButton);
        }
    }
    
    private string GetToolTipForTower(TowerButtonType towerType)
    {
        switch (towerType)
        {
            case TowerButtonType.WhiteBloodCell:
                return "Shoots projectiles with high range";
            case TowerButtonType.HeatTower:
                return "Burns enemies within range";
            case TowerButtonType.SlowTower:
                return "Slows Enemies within range";
            case TowerButtonType.AcidTower:
                return "Fires acid that poisons enemies";
            default:
                return string.Empty;
        }
    }
    
}
