using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ChildController : MonoBehaviour
{
    // Child Variables
    [SerializeField]
    private float childSpeed = 5.0f;
    [SerializeField]
    private float stopDistance = 1.0f;
    [SerializeField]
    private float followDistance = 5.0f;
    [SerializeField]
    private int health = 15;
    public Rigidbody2D rb;
    [SerializeField]
    private float trapImmunity = 0.5f;
    [SerializeField]
    // Yep we're defining that here, deal with it
    private int trapDamage = 1;
    [SerializeField]
    private float winDistance = 3.0f;

    private float curTrapImmunity = 0.0f;

    private ChildStateController stateSwitcher;
    private ChildHealth childHealth;
    private GameObject fairyLeader;
    private Vector2 houseLocation;

    // Animation Variables
    public Animator animator;
    private float horizontalInput;
    private float verticalInput;
    private float moving;


    // Start is called before the first frame update
    void Start()
    {
        stateSwitcher = GetComponent<ChildStateController>();
        childHealth = GetComponent<ChildHealth>();
        fairyLeader = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        ChildStateController.State currentState = stateSwitcher.currentState;
        if (currentState == ChildStateController.State.Follow)
        {
            MoveChild();
        }
        else if (currentState == ChildStateController.State.InTrap)
        {
            InTrap();
        }
        else if (currentState == ChildStateController.State.FollowEvil)
        {
            MoveChild();
        }
        else if (currentState == ChildStateController.State.Idle)
        {
            Idle();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "House")
        {
            Win();
        }

    }
    void MoveChild()
    {
        // Move the child towards the leader unless they're within stop distance
        Vector3 leaderPos = fairyLeader.GetComponent<Transform>().position;
        float distance = Vector2.Distance(transform.position, leaderPos);
        //rb.velocity = new Vector2(transform.position.x, transform.position.y);

        if (distance > stopDistance)
        {
            Vector2 direction = (leaderPos - transform.position).normalized;
            transform.Translate(direction * childSpeed * Time.deltaTime);
            moving = 1;

            horizontalInput = direction.x;
            verticalInput = direction.y;
        } 
        else {
            moving = 0;
        }

        // And if we're too far away then we become idle
        if (Vector2.Distance(fairyLeader.GetComponent<Transform>().position, transform.position) >= followDistance)
        {
            moving = 0;
            stateSwitcher.currentState = ChildStateController.State.Idle;
        }


        // Animations
        animator.SetFloat("Horizontal", horizontalInput);
        animator.SetFloat("Vertical", verticalInput);
        animator.SetFloat("Speed", moving);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
            animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
        }
    }

    void InTrap()
    {
        curTrapImmunity -= Time.deltaTime;
        if (curTrapImmunity < 0)
        {
            childHealth.TakeDamage(trapDamage);
            curTrapImmunity = trapImmunity;
        }
    }

    void Idle()
    {
        if (Vector2.Distance(fairyLeader.GetComponent<Transform>().position, transform.position) < followDistance)
        {
            stateSwitcher.currentState = ChildStateController.State.Follow;

        }
    }

    public void SetLeader(GameObject leader)
    {
        fairyLeader = leader;
    }

    void Win()
    {
        SceneManager.LoadScene("WinScreen");
    }
}
