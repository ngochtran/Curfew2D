using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataAggro : MonoBehaviour
{

    [SerializeField]
    private float speed = 6.0f;
    [SerializeField]
    private float dashSpeed = 8.0f;
    [SerializeField]
    private float dashDistanceTrigger = 3.0f;
    [SerializeField]
    private float dashDistance = 3.0f;
    [SerializeField]
    private float dashDelay = 1.0f;

    private EnemyStateSwitcher stateSwitcher;
    private float curDashDelay;
    private float dashTime;
    private float curDashDuration = 0.0f;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        stateSwitcher = GetComponent<EnemyStateSwitcher>();
        // Find the dash time
        dashTime = dashDistance / dashSpeed;
        curDashDelay = 1.0f;
        Debug.Log(dashTime);
    }

    // Update is called once per frame
    void Update()
    {

        // Get the current state
        EnemyStateSwitcher.State enemyState = stateSwitcher.currentState;
        // and move the llama towards the player if we're in aggro mode
        if (enemyState == EnemyStateSwitcher.State.Aggro)
        {

            // Switch to DashWarmup state if we're within range
            Vector2 childPos = GameObject.Find("Child").GetComponent<Transform>().position;
            Vector2 curPos = new Vector2(transform.position.x, transform.position.y);
            if (Vector2.Distance(childPos, transform.position) <= dashDistanceTrigger)
            {
                stateSwitcher.currentState = EnemyStateSwitcher.State.DashWarmup;
            }
            // Otherwise aggro
            else
            {
                // Create a vector pointing towards the child
                Vector2 direction = (childPos - curPos).normalized;

                // Move towards the child
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }
        // if we're in dash warmup, do dash warmup
        else if (enemyState == EnemyStateSwitcher.State.DashWarmup)
        {
            DashWarmup();
        }
        // and if we're dashing, do that
        else if (enemyState == EnemyStateSwitcher.State.Dashing)
        {
            Dash();
        }
    }

    // Decreases the current time and switches to our next mode when we're done.
    void DashWarmup()
    {
        curDashDelay -= Time.deltaTime;
        if (curDashDelay <= 0)
        {
            // Switch modes
            stateSwitcher.currentState = EnemyStateSwitcher.State.Dashing;
            // And reset our cooldown
            curDashDelay = dashDelay;
            // And finally set the correct direction
            Vector2 childPos = GameObject.Find("Child").GetComponent<Transform>().position;
            Vector2 curPos = new Vector2(transform.position.x, transform.position.y);

            direction = (childPos - curPos).normalized;
        }
    }

    // Move the llama forward to the target location at our given speed
    void Dash()
    {
        // Decrease our time
        curDashDuration -= Time.deltaTime;
        // Go back to aggro if we've used up all of our time
        if (curDashDuration <= 0)
        {
            stateSwitcher.currentState = EnemyStateSwitcher.State.Aggro;
            curDashDuration = dashTime;
        }
        // Otherwise, dash!
        else
        {
            transform.Translate(direction * dashSpeed * Time.deltaTime);
        }
    }
}