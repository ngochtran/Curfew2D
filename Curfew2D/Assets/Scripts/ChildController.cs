using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public float childSpeed = 5.0f;
    public float stopDistance = 1.0f;
    public int health = 15;
    public float invulnerableDuration = 1.0f;

    private float currentInvulnerability = 0.0f;

    // Update is called once per frame
    void Update()
    {
        MoveChild();

        // Also decrease the invulnerability period
        if (currentInvulnerability > 0)
        {
            currentInvulnerability -= Time.deltaTime;
        }
    }

    void MoveChild()
    {
        // Move the child towards the player unless they're within stop distance
        Vector3 playerPos = GameObject.Find("Player").GetComponent<Transform>().position;
        float distance = Vector2.Distance(transform.position, playerPos);

        if (distance > stopDistance)
        {
            Vector2 direction = (playerPos - transform.position).normalized;
            transform.Translate(direction * childSpeed * Time.deltaTime);
        }
    }

    void TakeDamage(int damage)
    {
        // Only take damage if we don't have invincibility frames
        if (currentInvulnerability <= 0)
        {
            currentInvulnerability = invulnerableDuration;
            health -= damage;
            if (health < 1)
            {
                Die();
            }
        }
    }

    void Die()
    {

    }
}
