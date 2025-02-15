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
public class LevelController : MonoBehaviour 
{

    public static float PlayerHealth = 0;
    public static int PlayerCurrency = 0;

    // TODO: Contain level-specific things here
    // Like something that handles waves/enemy spawning
    // Current player gold, etc
    [SerializeField]
    private Path path;

    public Path Path => path;

    void Start()
    {
        startWaveButton.onClick.AddListener(OnStartWaveButtonPressed);
        PlayerCurrency = 100;
    }

    #region Enemy Wave Spawning

    public Button startWaveButton;

    private bool waveStarted = false;

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
        if(!waveStarted)
            StartCoroutine(SpawnNextWave());
    }

    public IEnumerator SpawnNextWave()
    {
        waveStarted = true;
        enemiesSpawned = 0;

        if (Waves[currentWaveNum] != null)
        {
            foreach (int enemy in Waves[currentWaveNum].Enemies)
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
        currentWaveNum++;
        waveStarted = false;
    }


    #endregion
}
