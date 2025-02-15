using UnityEngine;
using UnityEngine.Splines;

public class BasicEnemy : MonoBehaviour
{
    /// <summary>
    /// Enemy Stats
    /// </summary>
    public float Health = 100;
    public float Speed = 5;
    public float Damage = 1;
    public int currencyDrop = 25;

    public SplineAnimate splineAnimator;
    private LevelController levelController;

    void Start()
    {
        levelController = FindFirstObjectByType<LevelController>();
        splineAnimator.Completed += EndOfPath;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndOfPath()
    {
        levelController.PlayerHealth -= Damage;
        levelController.enemiesSpawned--;
        levelController.CheckWaveComplete();
        levelController.UpdateMeters();
        Destroy(gameObject);
    }

    private void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private void Die()
    {
        levelController.PlayerCurrency += currencyDrop;
        levelController.enemiesSpawned--;
        levelController.UpdateMeters();
        levelController.CheckWaveComplete();
    }
}
