using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildHealth : MonoBehaviour
{

    [SerializeField]
    private int maxHealth = 10;

    private int health;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health < 1)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Child Died");
    }

    public void Heal(int amount)
    {
        health += amount;
        healthBar.SetHealth(health);
        if (health > maxHealth)
        {
            health = maxHealth;
            healthBar.SetHealth(health);
        }
    }
}
