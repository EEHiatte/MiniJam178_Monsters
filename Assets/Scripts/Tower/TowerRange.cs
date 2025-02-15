using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rangeSprite;

    private List<BasicEnemy> enemiesInRange = new List<BasicEnemy>();
    
    public void ShowRangeIndicator(bool showRange)
    {
        rangeSprite.enabled = showRange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyScript = other.gameObject.GetComponent<BasicEnemy>();
        if (enemyScript == null)
        {
            return;
        }

        enemiesInRange.Add(enemyScript);
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        var enemyScript = other.gameObject.GetComponent<BasicEnemy>();
        if (enemyScript == null)
        {
            return;
        }

        if (enemiesInRange.Contains(enemyScript))
        {
            enemiesInRange.Remove(enemyScript);
        }
    }

    public BasicEnemy GetTarget(TargetingType targetingType)
    {
        if (enemiesInRange.Count <= 0)
        {
            return null;
        }
        
        switch (targetingType)
        {
            case TargetingType.None:
                return null;
                
            case TargetingType.First:
                return enemiesInRange.First();
            
            case TargetingType.Last:
                return enemiesInRange.Last();

            case TargetingType.Closest:
            {
                return GetClosestEnemy();
            }
            default:
                return null;
        }
    }

    public List<BasicEnemy> GetAllEnemiesInRange()
    {
        if (enemiesInRange.Count < 0)
        {
            return null;
        }

        return enemiesInRange;
    }

    private BasicEnemy GetClosestEnemy()
    {
        var myTransform = transform.position;
        float closestDistance = Mathf.Infinity;
        BasicEnemy closestEnemy = null;

        float cachedDistance;
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            cachedDistance = Mathf.Abs(Vector3.Distance(myTransform, enemiesInRange[i].transform.position));

            if (cachedDistance < closestDistance)
            {
                closestDistance = cachedDistance;
                closestEnemy = enemiesInRange[i];
            }
        }

        return closestEnemy;
    }
}


public enum TargetingType
{
    None,
    First,
    Last,
    Closest
}
