using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for a level's handling.
/// </summary>
public class LevelController : MonoBehaviour 
{

    public float PlayerHealth = 0;
    public float PlayerMaxHealth = 0;
    public TextMeshProUGUI healthMeter;
    public TextMeshProUGUI currencyMeter;
    public TextMeshProUGUI StartButtonText;
    public TextMeshProUGUI FFButtonText;

    public GameObject LevelCompleteMenu;
    public GameObject PauseMenu;
    public bool paused = false;

    public AudioClip waveStart;
    public AudioClip ekgBlip;
    public AudioClip FlatLine;

    public AudioSource AudioPlayer;

    public int GoldSpent;
    public int MonstersKilled;
    public int TowersBuilt;
    
    private int _playerCurrency;

    public int GameState = -1;
    
    public Color TintColor = Color.white;

    public int PlayerCurrency
    {
        get => _playerCurrency;
        set
        {
            _playerCurrency = value;
            UpdateMeters();
            OnCurrencyUpdate?.Invoke(this, null);
        }
    }

    public event EventHandler OnCurrencyUpdate; 
    
    // TODO: Contain level-specific things here
    // Like something that handles waves/enemy spawning
    // Current player gold, etc
    [SerializeField]
    private Path path;
    
    [SerializeField]
    private Background background;

    public Path Path => path;

    void Start()
    {
        GameState = 0;
        PlayerMaxHealth = PlayerHealth;
        UpdateMeters();
        startWaveButton.onClick.AddListener(OnStartWaveButtonPressed);
        fastForwardButton.onClick.AddListener(OnFastForwardButtonPressed);
        PlayerCurrency = 30;
        StartButtonText.text = "Start Wave " + (currentWaveNum + 1).ToString();
        switch (PlayerPrefs.GetInt("CurrentLevel"))
        {
            case 0:
                Waves = Level1Waves;
                break;
            case 1:
                Waves = Level2Waves;
                break;
            case 2:
                Waves = Level3Waves;
                break;
        }
    }

    void Update()
    {
        if (GameState != 0 || paused)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = true;
            PauseMenu.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnFastForwardButtonPressed();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnStartWaveButtonPressed();
        }
    }

    #region Enemy Wave Spawning

    public Button startWaveButton;

    public Button fastForwardButton;

    private bool waveStarted = false;

    [Serializable]
    public class Wave
    {
        public float spawnRate;
        public List<int> Enemies = new List<int>();
    }

    [SerializeField]
    private List<Wave> Waves = new List<Wave>();

    public List<Wave> Level1Waves = new List<Wave>();
    public List<Wave> Level2Waves = new List<Wave>();
    public List<Wave> Level3Waves = new List<Wave>();

    public List<GameObject> EnemyPrefabs;

    private int currentWaveNum = 0;

    //When an enemy dies or makes it to the end, decrement this number by one
    public int enemiesSpawned = 0;

    public void OnStartWaveButtonPressed()
    {
        if (!waveStarted)
        {
            StartButtonText.text = "Wave " + (currentWaveNum + 1).ToString();
            startWaveButton.enabled = false;
            StartCoroutine(SpawnNextWave());
            AudioPlayer.volume = 0.3f;
            AudioPlayer.PlayOneShot(waveStart);
        }
    }

    private int gameSpeed = 0;

    public void OnFastForwardButtonPressed()
    {
        if (gameSpeed == 0)
        {
            gameSpeed = 1;
            Time.timeScale = 1.5f;
            FFButtonText.text = ">>";
        }
        else if (gameSpeed == 1)
        {
            gameSpeed = 2;
            Time.timeScale = 2;
            FFButtonText.text = ">>>";
        }
        else if(gameSpeed == 2)
        {
            gameSpeed = 0;
            Time.timeScale = 1;
            FFButtonText.text = ">";
        }
    }

    public IEnumerator SpawnNextWave()
    {
        waveStarted = true;
        enemiesSpawned = 0;

        if (currentWaveNum < Waves.Count)
        {
            foreach (int enemy in Waves[currentWaveNum].Enemies)
            {
                //SpawnEnemyPrefab
                GameObject t_enemy = Instantiate(EnemyPrefabs[enemy], new Vector3(1000,1000,1000), EnemyPrefabs[enemy].transform.rotation);
                t_enemy.transform.parent = Path.SplineContainer.transform;
                BasicEnemy basicEnemy = t_enemy.GetComponent<BasicEnemy>();

                basicEnemy.splineAnimator.Container = Path.SplineContainer;
                basicEnemy.splineAnimator.MaxSpeed = basicEnemy.CurrentSpeed;
                basicEnemy.splineAnimator.Play();
                
                Path.AddDisplacementObject(basicEnemy.transform, basicEnemy.radius, basicEnemy.displacement);
                basicEnemy.OnDeath += () => Path.RemoveDisplacementObject(basicEnemy.transform);

                enemiesSpawned++;
                if(currentWaveNum < Waves.Count)
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
        
        StopAllCoroutines();

        if (currentWaveNum < Waves.Count)
        {
            StartButtonText.text = "Start Wave " + (currentWaveNum + 1).ToString();
            waveStarted = false;
            startWaveButton.enabled = true;
        }
        else
        {
            startWaveButton.gameObject.SetActive(false);
            LevelComplete();
        }
    }

    public void LevelComplete()
    {
        if (PlayerHealth <= 0)
        {
            GameState = 1;
        }
        else
        {
            GameState = 2;
            PlayerPrefs.SetInt("CompletedLevel", PlayerPrefs.GetInt("CurrentLevel"));
        }
        LevelCompleteMenu.gameObject.SetActive(true);
    }

    public void LevelFailed()
    {
        GameState = 1;
        LevelCompleteMenu.gameObject.SetActive(true);
        startWaveButton.gameObject.SetActive(false);
        AudioPlayer.loop = true;
        AudioPlayer.clip = FlatLine;
        AudioPlayer.volume = 0.35f;
        AudioPlayer.Play();
    }
    #endregion


    public void UpdateMeters()
    {
        currencyMeter.text = PlayerCurrency.ToString();
        healthMeter.text = PlayerHealth.ToString();
    }
}
