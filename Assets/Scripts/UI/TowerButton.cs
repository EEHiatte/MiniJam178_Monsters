using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private TowerButtonType towerType;
    [SerializeField] private GameObject lockedDimmer;
    [SerializeField] private Button button;

    [SerializeField] private Tower towerToSpawn;
    [SerializeField] private bool unlocked;
    
    public TowerButtonType TowerType => towerType;
    public Button Button => button;
    public Tower TowerToSpawn => towerToSpawn;
    
    private void Awake()
    {
        SetUnlocked(unlocked);
    }

    public void SetUnlocked(bool setUnlocked)
    {
        unlocked = setUnlocked;
        lockedDimmer.gameObject.SetActive(!unlocked);
        button.interactable = unlocked;
    }
}
