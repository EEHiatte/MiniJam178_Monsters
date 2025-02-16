using System.Collections;
using UnityEngine;

public class PassiveDamagingObject : MonoBehaviour
{
    [SerializeField] private PassiveObjectType objectType;
    [SerializeField] private float damageAmount;
    [SerializeField] private Collider2D collider;
    
    [Header("Values for Trap")]
    [SerializeField] private int damageTimes;

    [Header("Values for Acid")]
    [SerializeField] private int lifeDuration;
    [SerializeField] private int poisonTickAmount;
    private float currentTimer;
    
    public enum PassiveObjectType
    {
        Acid,
        Trap
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemyComponent = other.gameObject.GetComponent<BasicEnemy>();

        if (enemyComponent == null)
        {
            return;
        }

        switch (objectType)
        {
            case PassiveObjectType.Acid:
            {
                enemyComponent.OnPoison(poisonTickAmount, (int)damageAmount);
                break;
            }
        }
    }

    private void Start()
    {
        if (objectType == PassiveObjectType.Acid)
        {
            currentTimer = lifeDuration;
        }
    }
    
    private void Update()
    {
        if (objectType == PassiveObjectType.Acid)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f && collider.enabled)
            {
                collider.enabled = false;
                StartCoroutine(Destroy());
            }
        }
    }

    private IEnumerator Destroy()
    {
        float time = .5f;
        float currentTime = 0f;
        Vector3 startingScale = this.transform.localScale;
        
        while (currentTime < time)
        {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
            this.transform.localScale = Vector3.Lerp(startingScale, Vector3.zero, currentTime / time);    
        }

        Destroy(this.gameObject);
    }
}


