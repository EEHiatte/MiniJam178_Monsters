using UnityEngine;

public class TowerRange : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rangeSprite;

    public void ShowRangeIndicator(bool showRange)
    {
        rangeSprite.enabled = showRange;
    }
}
