using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField]
    private int health = 10;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 1)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        // If we're luring the child then we need to set a new leader before expiring
        if (GetComponent<EnemyStateSwitcher>().currentState == EnemyStateSwitcher.State.Luring)
        {
            // Set the new leader to the player
            GameObject player = GameObject.Find("Player");
            GameObject.Find("Child").GetComponent<ChildController>().SetLeader(player);
        }
    }
}
