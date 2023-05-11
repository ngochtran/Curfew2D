using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ChildController : MonoBehaviour
{
    // Child Variables
    public float childSpeed = 5.0f;
    public float stopDistance = 1.0f;
    public int health = 15;
    public Rigidbody2D rb;

    private ChildStateController stateSwitcher;
    private GameObject fairyLeader;

    // Animation Variables
    public Animator animator;
    private float horizontalInput;
    private float verticalInput;
    private float moving;


    // Start is called before the first frame update
    void Start()
    {
        stateSwitcher = GetComponent<ChildStateController>();
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
        
    }

    public void SetLeader(GameObject leader)
    {
        fairyLeader = leader;
    }
}
