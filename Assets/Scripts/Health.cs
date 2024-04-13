using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;

    float currentHealth;

    public delegate void OnDeath();
    public OnDeath onDeath;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > health)
        {
            currentHealth = health;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        if (onDeath != null)
        {
            onDeath();
        }

        Destroy(gameObject);
    }
}
