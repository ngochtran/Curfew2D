using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : MonoBehaviour
{
    public float speed = 6.0f;
    public float maxIdleTime = 4.0f;
    public float maxWalkingTime = 1.0f;

    private float idleTime = 0.0f;
    private float walkingTime = 0.0f;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        ResetIdleTime();
        ResetWalkingTime();
        ResetDirection();

    }

    // Update is called once per frame
    void Update()
    {
        EnemyStateSwitcher.State currentState = GetComponent<EnemyStateSwitcher>().currentState;
        if (currentState == EnemyStateSwitcher.State.Idle)
        {
            // Decrease idle time if we have some remaining
            if (idleTime > 0)
            {
                idleTime -= Time.deltaTime;
            }
            // Otherwise move in our direction and decrease walking time
            else if (walkingTime > 0)
            {
                transform.Translate(direction * speed * Time.deltaTime);
                walkingTime -= Time.deltaTime;
            }
            // Otherwise reset everything and start again
            else
            {
                ResetIdleTime();
                ResetWalkingTime();
                ResetDirection();
            }
        }
    }

    void ResetIdleTime()
    {
        idleTime = Random.Range(0, maxIdleTime);
    }

    void ResetWalkingTime()
    {
        walkingTime = Random.Range(0, maxWalkingTime);
    }

    void ResetDirection()
    {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}