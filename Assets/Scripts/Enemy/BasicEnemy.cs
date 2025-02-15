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

    void Start()
    {
        splineAnimator.Completed += EndOfPath;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndOfPath()
    {
        LevelController.PlayerHealth -= Damage;
        LevelController.enemiesSpawned--;
        FindFirstObjectByType<LevelController>().CheckWaveComplete();
        Destroy(gameObject);
    }

    private void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private void Die()
    {
        LevelController.PlayerCurrency += currencyDrop;
        LevelController.enemiesSpawned--;
        FindFirstObjectByType<LevelController>().CheckWaveComplete();
    }
}
