using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFairyController : MonoBehaviour
{

    [SerializeField]
    private float spellDuration = 1.0f;
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float goPastTrapDistance = 2.0f;

    private EnemyStateSwitcher stateSwitcher;
    private float curSpellDuration;
    private GameObject nearestTrap;
    private Vector2 direction;
    private bool pastTrap = false;
    private float gonePastTrapDistance = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        stateSwitcher = GetComponent<EnemyStateSwitcher>();
        curSpellDuration = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        EnemyStateSwitcher.State enemyState = stateSwitcher.currentState;
        // Do the stupid workaround to set it to spell warmup
        if (enemyState == EnemyStateSwitcher.State.Aggro)
        {
            stateSwitcher.currentState = EnemyStateSwitcher.State.SpellWarmup;
        }
        else if (enemyState == EnemyStateSwitcher.State.SpellWarmup)
        {
            SpellWarmup();
        }
        else if (enemyState == EnemyStateSwitcher.State.Luring)
        {
            Lure();
        }
    }

    void SpellWarmup()
    {
        curSpellDuration += Time.deltaTime;
        if (curSpellDuration >= spellDuration)
        {
            curSpellDuration = 0.0f;
            stateSwitcher.currentState = EnemyStateSwitcher.State.Luring;
            // Also find the nearest trap
            FindNearestTrap();
        }
    }

    void FindNearestTrap()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Trap");
        GameObject closest = objects[0];
        Vector2 pos = transform.position;
        for (int i = 0; i < objects.Length; i++ )
        {
            if (Vector2.Distance(pos, objects[i].transform.position) < Vector2.Distance(pos, closest.transform.position))
            {
                closest = objects[i];
            }
        }
        nearestTrap = closest;
        direction = (nearestTrap.transform.position - transform.position).normalized;
    }

    void Lure()
    {
        if (!pastTrap)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else if (gonePastTrapDistance <= goPastTrapDistance)
        {
            gonePastTrapDistance += speed * Time.deltaTime;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // We're past the trap if we hit it and we're in luring mode
        if (collision.CompareTag("Trap"))
        {
            if (stateSwitcher.currentState == EnemyStateSwitcher.State.Luring)
            {
                pastTrap = true;
            }
        }
    }
}
