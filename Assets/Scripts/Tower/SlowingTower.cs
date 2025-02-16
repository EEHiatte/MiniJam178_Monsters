using UnityEngine;

public class SlowingTower : DamagingTowerBase
{
    public override bool OnTryDamage(DamageInfo damageInfo)
    {
        var enemiesInRange = damageInfo.TowerRange.GetAllEnemiesInRange();

        if (enemiesInRange == null)
        {
            return false;
        }
        
        foreach (var basicEnemy in enemiesInRange)
        {
            basicEnemy.OnSlow((int)damageInfo.Damage);
        }

        return true;
    }
}
