public class DOTAoeTower : DamagingTowerBase
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
            if (basicEnemy != null) // they could have gotten killed
            {
                basicEnemy.TakeDamage((int)damageInfo.Damage);
            }
        }

        return true;
    }
}
