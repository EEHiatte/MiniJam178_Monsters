using UnityEngine;

public class AcidTower : DamagingTowerBase
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private PassiveDamagingObject _poisonObject;
    
    public override bool OnTryDamage(DamageInfo damageInfo)
    {
        var target = damageInfo.TowerRange.GetTarget(TargetingType.First);
        if (target == null)
        {
            return false;
        }

        _explosionEffect.Play();

        var spawnPos = new Vector3(target.transform.position.x, target.transform.position.y, 0f);
        var acid = Instantiate(_poisonObject, spawnPos, Quaternion.identity);
        return true;
    }
}
