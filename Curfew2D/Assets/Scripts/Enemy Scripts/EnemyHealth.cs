using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField]
    private int ropeChoice = 2;
    [SerializeField]
    private int candyChoice = 7;

    [SerializeField]
    private GameObject rope;
    [SerializeField]
    private GameObject candy;
    [SerializeField]
    private int maxHealth = 100;
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
        Destroy(gameObject);
        // If we're luring the child then we need to set a new leader before expiring
        EnemyStateSwitcher.State currentState = GetComponent<EnemyStateSwitcher>().currentState;
        if (currentState == EnemyStateSwitcher.State.Luring)
        {
            // Set the new leader to the player
            GameObject player = GameObject.Find("Player");
            GameObject.Find("Child").GetComponent<ChildController>().SetLeader(player);
        }

        // But if we're a llama we got to maybe drop an item
        if (GetComponent<PinataAggro>() != null)
        {
            DropItem();
        }
    }

    void DropItem()
    {
        Vector2 pos = GetComponent<Transform>().position;
        Quaternion rot = new Quaternion();
        int choice = Random.Range(0, 10);
        if (choice <= ropeChoice)
        {
            Instantiate(rope, pos, rot);
        }
        else if (choice <= candyChoice)
        {
            Instantiate(candy, pos, rot);
        }
    }
}
