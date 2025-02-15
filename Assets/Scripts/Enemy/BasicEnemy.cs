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

    public SplineAnimate splineAnimator;

    // Update is called once per frame
    void Update()
    {

    }

    private void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private void Die()
    {
        LevelController.enemiesSpawned--;
    }
}
