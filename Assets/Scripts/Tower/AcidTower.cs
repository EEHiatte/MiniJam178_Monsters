using UnityEngine;

public class AcidTower : DamagingTowerBase
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private PassiveDamagingObject _poisonObject;
    [SerializeField] private AudioSource _slimeAudio;
    
    public override bool OnTryDamage(DamageInfo damageInfo)
    {
        var target = damageInfo.TowerRange.GetTarget(TargetingType.First);
        if (target == null)
        {
            return false;
        }

        _explosionEffect.Play();

        var spawnPos = new Vector3(target.transform.position.x, target.transform.position.y, 0f);
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        var acid = Instantiate(_poisonObject, spawnPos, randomRotation);
        _slimeAudio.Play();
        return true;
    }
}
