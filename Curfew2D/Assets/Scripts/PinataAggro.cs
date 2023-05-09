using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataAggro : MonoBehaviour
{

    public float speed = 6.0f;
    public int health = 1;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Destroy if we're dead
        if (health < 1)
        {
            Destroy(gameObject);
        }

        // Get the current state
        EnemyStateSwitcher.State enemyState = GetComponent<EnemyStateSwitcher>().currentState;
        // and move the llama towards the player if we're in aggro mode
        if (enemyState == EnemyStateSwitcher.State.Aggro)
        {
            // Get the child's position and create a vector pointing towards them
            Vector3 childPos = GameObject.Find("Child").GetComponent<Transform>().position;
            Vector3 direction = Vector3.Normalize(childPos - transform.position);

            // Move towards the child
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
    }
}