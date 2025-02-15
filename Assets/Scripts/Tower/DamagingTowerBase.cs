using UnityEngine;

public abstract class DamagingTowerBase : MonoBehaviour
{
    public struct DamageInfo
    {
        public DamageInfo(TowerRange range, float damage)
        {
            TowerRange = range;
            Damage = damage;
        }
        public TowerRange TowerRange;
        public float Damage;
    }
    public abstract bool OnTryDamage(DamageInfo damageInfo);
}
