using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float currentHealth;
    public float CurrentHealth => currentHealth;

    [SerializeField]
    private float maxHealth;
    public float MaxHealth => maxHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0.0f, maxHealth);

        if (currentHealth <= 0)
        {
            Destroy();
        }
    }

    public virtual void HealDamage(int healValue)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + healValue, 0, maxHealth);
        }
    }
}
