using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

/// <summary>
/// Controller for a level's handling.
/// </summary>
public class LevelController : Controller 
{

    #region Enemy Wave Spawning

    [Serializable]
    public class Wave
    {
        public float spawnRate;
        public List<int> Enemies = new List<int>();
    }

    [SerializeField]
    public List<Wave> Waves = new List<Wave>();

    public List<GameObject> EnemyPrefabs;

    private int currentWaveNum = 0;

    //When an enemy dies or makes it to the end, decrement this number by one
    public static int enemiesSpawned = 0;

    public void OnStartWaveButtonPressed()
    {
        StartCoroutine(SpawnNextWave());
    }

    public IEnumerator SpawnNextWave()
    {
        enemiesSpawned = 0;

        foreach(int enemy in Waves[currentWaveNum].Enemies)
        {
            //SpawnEnemyPrefab
            GameObject t_enemy = Instantiate(EnemyPrefabs[enemy]);
            BasicEnemy basicEnemy = t_enemy.GetComponent<BasicEnemy>();

            basicEnemy.splineAnimator.Container = Path.SplineContainer;
            basicEnemy.splineAnimator.MaxSpeed = basicEnemy.Speed;
            basicEnemy.splineAnimator.Play();

            enemiesSpawned++;
            yield return new WaitForSeconds(Waves[currentWaveNum].spawnRate);
        }

        yield return null;
    }

    public void CheckWaveComplete()
    {
        if(enemiesSpawned == 0)
        {
            EndWave();
        }
    }

    public void EndWave()
    {
        //Re-enable next wave start button
    }


    #endregion

    // TODO: Contain level-specific things here
    // Like something that handles waves/enemy spawning
    // Current player gold, etc
    [SerializeField]
    private Path path;
    
    public Path Path => path;
    
    protected override bool RegisterToManager()
    {
        return GameManager.Instance.RegisterController(this);
    }

    protected override bool UnregisterFromManager()
    {
        return GameManager.Instance.UnregisterController(this);
    }
}
