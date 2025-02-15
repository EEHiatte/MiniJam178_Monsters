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
            basicEnemy.TakeDamage((int)damageInfo.Damage);
        }

        return true;
    }
}
