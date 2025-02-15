using System.Collections;
using UnityEngine;

public class ShootingTowerDamage : DamagingTowerBase
{
    [SerializeField] private GameObject bulletPrefab;

    public override bool OnTryDamage(DamageInfo damageInfo)
    {
        var target = damageInfo.TowerRange.GetTarget(TargetingType.First);
        if (target == null)
        {
            return false;
        }

        StartCoroutine(FireBulletAtEnemy(target, damageInfo));
        return true;
    }

    private IEnumerator FireBulletAtEnemy(BasicEnemy target, DamageInfo damageInfo)
    {
        float firetime = 0.15f;
        float currentTime = 0;

        var startingPosition = this.transform.position;
        var endingPosition = new Vector3(target.gameObject.transform.position.x, target.gameObject.transform.position.y, 0f);

        var bullet = Instantiate(bulletPrefab, this.transform);
        bullet.transform.position = startingPosition;

        while (currentTime < firetime)
        {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
            var delta = currentTime / firetime;
            bullet.transform.position = Vector3.Lerp(startingPosition, endingPosition, delta);
        }

        Destroy(bullet.gameObject);
        if (target != null)
        {
            target.TakeDamage((int)damageInfo.Damage);
        }
    }
}
  
