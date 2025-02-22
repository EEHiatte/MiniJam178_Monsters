using System;
using TMPro;
using UnityEngine;using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TowerButtonType towerType;
    [SerializeField] private GameObject lockedDimmer;
    [SerializeField] private Button button;

    [SerializeField] private TextMeshProUGUI priceText;
    
    [SerializeField] private Tower towerToSpawn;
    [SerializeField] private bool unlocked;
    
    public TowerButtonType TowerType => towerType;
    public Button Button => button;
    public Tower TowerToSpawn => towerToSpawn;

    private int _cachedPlayerCurrency;
    private int _cachedPrice;

    public event EventHandler MouseOver;
    public event EventHandler MouseExit;
    
    private void Awake()
    {
        _cachedPrice = towerToSpawn.TowerCost;
        priceText.text = _cachedPrice.ToString();
        SetUnlocked(unlocked);
    }

    public void OnCurrencyUpdate(int currency)
    {
        _cachedPlayerCurrency = currency;
        priceText.color = _cachedPlayerCurrency >= _cachedPrice ? Color.white : Color.red;
        SetButtonInteractable();
    }

    public void SetUnlocked(bool setUnlocked)
    {
        unlocked = setUnlocked;
        lockedDimmer.gameObject.SetActive(!unlocked);
        priceText.gameObject.SetActive(unlocked);
        SetButtonInteractable();
    }

    private void SetButtonInteractable()
    {
        button.interactable = unlocked && _cachedPlayerCurrency >= _cachedPrice;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOver?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExit?.Invoke(this, EventArgs.Empty);
    }
}
