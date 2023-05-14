using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{

    [SerializeField]
    private int maxIdleCycles = 3;
    [SerializeField]
    private int minIdleCycles = 2;
    [SerializeField]
    private float digTime = 2.0f;
    [SerializeField]
    private GameObject trap;

    private int chosenIdleCycles;
    private int curCycleCount;
    private float curDigTime;

    private EnemyStateSwitcher stateSwitcher;
    private EnemyIdle enemyIdle;

    // Animation Variables
    public Animator animator;
    private bool isDigging;
    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        stateSwitcher = GetComponent<EnemyStateSwitcher>();
        enemyIdle = GetComponent<EnemyIdle>();
        ChooseCycles();
        curCycleCount = 0;
        curDigTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCycleCount();
        // After a certain number of idle cycles we want the mole to start digging, yeah? And then it goes back to idle. Cool.
        if (curCycleCount == chosenIdleCycles)
        {
            enemyIdle.ResetIdleCycles();
            curCycleCount = 0;
            UpdateCycleCount();
            stateSwitcher.currentState = EnemyStateSwitcher.State.Digging;
        }

        // And do the usual state stuff
        EnemyStateSwitcher.State enemyState = stateSwitcher.currentState;
        if (enemyState == EnemyStateSwitcher.State.Idle)
        {
            isRunning = true;
            isDigging = false;
        }
        else if (enemyState == EnemyStateSwitcher.State.Digging)
        {
            isRunning = false;
            isDigging = true;
            Dig();
        }
        animator.SetBool("isDigging", isDigging);
        animator.SetBool("isRunning", isRunning);
    }

    void Dig()
    {
        curDigTime += Time.deltaTime;
        if (curDigTime > digTime)
        {
            // Switch state, reset curDigTime and spawn trap
            curDigTime = 0.0f;
            stateSwitcher.currentState = EnemyStateSwitcher.State.Idle;
            Instantiate(trap, transform.position, transform.rotation);
        }
    }

    void ChooseCycles()
    {
        chosenIdleCycles = Random.Range(minIdleCycles, maxIdleCycles + 1);
    }

    void UpdateCycleCount()
    {
        curCycleCount = enemyIdle.idleCycles;
    }
}
