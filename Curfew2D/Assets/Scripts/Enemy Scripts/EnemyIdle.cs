using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : MonoBehaviour
{
    [SerializeField]
    private float speed = 6.0f;
    [SerializeField]
    private float maxIdleTime = 4.0f;
    [SerializeField]
    private float minIdleTime = 1.0f;
    [SerializeField]
    private float maxWalkingTime = 1.0f;
    [SerializeField]
    private float minWalkingTime = 0.5f;

    private float idleTime = 0.0f;
    private float walkingTime = 0.0f;
    private Vector2 direction;
    public int idleCycles = 0;

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
                // Also increment our idle cycles
                idleCycles++;
            }
        }
    }

    void ResetIdleTime()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
    }

    void ResetWalkingTime()
    {
        walkingTime = Random.Range(minWalkingTime, maxWalkingTime);
    }

    void ResetDirection()
    {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void ResetIdleCycles()
    {
        idleCycles = 0;
    }
}