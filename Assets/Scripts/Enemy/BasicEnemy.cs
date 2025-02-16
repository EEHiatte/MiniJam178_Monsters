using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;
using UnityEngine.Splines.Interpolators;

public class BasicEnemy : MonoBehaviour
{
    public float radius = 1f;
    public float displacement = 0.25f;

    /// <summary>
    /// Enemy Stats
    /// </summary>
    public float Health = 100;
    public float MaxHealth = 100;
    public float Speed = 5;
    public float Damage = 1;
    public int currencyDrop = 25;

    public SplineAnimate splineAnimator;
    private LevelController levelController;

    private Color tempColor;
    
    public UnityAction OnDeath;

    void Start()
    {
        levelController = FindFirstObjectByType<LevelController>();
        splineAnimator.Completed += EndOfPath;
        GetComponent<SpriteRenderer>().material.SetColor("_Tint", GetComponent<SpriteRenderer>().color);
        tempColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndOfPath()
    {
        OnDeath?.Invoke();
        levelController.PlayerHealth -= Damage;
        levelController.enemiesSpawned--;
        levelController.CheckWaveComplete();
        levelController.UpdateMeters();
        if (levelController.PlayerHealth <= 0)
        {
            levelController.LevelFailed();
        }
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        GetComponent<SpriteRenderer>().material.SetColor("_Tint", Color.Lerp(Color.white, tempColor, Health/MaxHealth));
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        levelController.MonstersKilled++;
        levelController.PlayerCurrency += currencyDrop;
        levelController.enemiesSpawned--;
        levelController.UpdateMeters();
        levelController.CheckWaveComplete();
        Destroy(gameObject);
    }
}
