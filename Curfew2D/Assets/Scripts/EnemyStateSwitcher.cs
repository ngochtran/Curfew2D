using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSwitcher : MonoBehaviour
{

    public enum State { Idle, Aggro };
    public State currentState;
    public float aggroRange = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the state to aggro if we enter the aggro range
        Vector2 childPos = GameObject.Find("Child").GetComponent<Transform>().position;
        if (Vector2.Distance(transform.position, childPos) < aggroRange)
        {
            currentState = State.Aggro;
        }
    }
}