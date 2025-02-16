using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
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
    [SerializeField] private float Speed = 5;
    public float Damage = 1;
    public int currencyDrop = 25;

    public SplineAnimate splineAnimator;
    private LevelController levelController;

    //Enemy Sounds
    public AudioClip damagePlayer;

    private Color tempColor;
    
    public UnityAction OnDeath;

    private float _speedModifier = 1.0f;
    private float _speedModifierDuration = 2.0f;
    private float _speedModifierCurrentTime = -1f;
    private bool slowed = false;
    
    public float CurrentSpeed => Speed * _speedModifier;
    private PoisonHelper _poisonHelper = new PoisonHelper();

    public class PoisonHelper
    {
        private float _tickTimer;
        private Queue<int> _poisonTicks = new();
        
        public void AddTicks(int ticks, int damageAmount)
        {
            for(int i = 0; i < ticks; ++i)
                _poisonTicks.Enqueue(damageAmount);
        }
        
        public void Update(float deltaTime)
        {
            if (_tickTimer > 0f)
                _tickTimer -= deltaTime;
        }

        public int TryTick()
        {
            if (_tickTimer <= 0f && _poisonTicks.Count > 0)
            {
                _tickTimer = .1f;
                return _poisonTicks.Dequeue();
            }

            return 0;
        }
        
        
    }

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
        if (_speedModifierCurrentTime > 0f)
        {
            _speedModifierCurrentTime -= Time.deltaTime;
            if (_speedModifierCurrentTime <= 0f)
            {
                _speedModifier = 1.0f;
                ModifySpeed();
            }
        }

        _poisonHelper.Update(Time.deltaTime);
        var poisonDamage = _poisonHelper.TryTick();
        if (poisonDamage > 0)
        {
            TakeDamage(poisonDamage);
        }
    }

    private void ModifySpeed()
    {
        // Yo fuck spline.
        splineAnimator.Pause();

        var elapsedTime = splineAnimator.ElapsedTime;
        var previousDuration = splineAnimator.Duration;
        splineAnimator.MaxSpeed = CurrentSpeed;

        // Just rebuilding the new needed elapsed time based on what the previous one was with old duration.
        var updatedDuration = splineAnimator.Duration;
        var newModifiedElapsedTime = elapsedTime * updatedDuration / previousDuration;
        
        splineAnimator.ElapsedTime = newModifiedElapsedTime;
        splineAnimator.Play();
    }
    

    public void EndOfPath()
    {
        OnDeath?.Invoke();
        levelController.PlayerHealth -= Damage;
        levelController.enemiesSpawned--;
        levelController.CheckWaveComplete();
        levelController.UpdateMeters();
        levelController.AudioPlayer.PlayOneShot(damagePlayer);
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

    public void OnSlow(int slowFactor)
    {
        _speedModifierCurrentTime = _speedModifierDuration;
        if (slowed)
        {
            return;
        }
        
        _speedModifier = 1.0f - (slowFactor / 100f);
        ModifySpeed();
        slowed = true;
    }

    public void OnPoison(int ticks, int damageAmountPerTick)
    {
        _poisonHelper.AddTicks(ticks, damageAmountPerTick);
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
