using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [SerializeField]
    private float itemUseDistance = 2.0f;
    [SerializeField]
    private int healAmouont = 2;
    [SerializeField]
    private float freeDistance = 1.0f;

    private Inventory inventory;
    private Transform childTransform;
    private Bounds childBounds;
    private ChildStateController.State childState;
    private ChildHealth childHealth;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        GameObject child = GameObject.Find("Child");
        childTransform = child.GetComponent<Transform>();
        childState = child.GetComponent<ChildStateController>().currentState;
        childHealth = child.GetComponent<ChildHealth>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject child = GameObject.Find("Child");
        childTransform = child.GetComponent<Transform>();
        Vector2 playerPos = player.GetComponent<Transform>().position;
        Vector2 childPos = childTransform.position;

        if (Vector2.Distance(childPos, playerPos) < itemUseDistance)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UseRope();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseCandy();
            }
        }
    }

    void Clicked()
    {
        GameObject child = GameObject.Find("Child");
        childTransform = child.GetComponent<Transform>();
        childBounds = child.GetComponent<CapsuleCollider2D>().bounds;

        Vector2 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = player.GetComponent<Transform>().position;
        Vector2 childPos = childTransform.position;

        if (childBounds.Contains(mouseLocation))
        {
            if (Vector2.Distance(childPos, playerPos) < itemUseDistance)
            {
                // Use a rope if the child is trapped, otherwise use a candy.
                ChildStateController.State curState = child.GetComponent<ChildStateController>().currentState;
                if (curState == ChildStateController.State.InTrap)
                {
                    // Try to use a rope. If we have a rope.
                    if (inventory.UseItem("Rope"))
                    {
                        Vector2 direction = (playerPos - childPos).normalized;
                        child.transform.Translate(direction * freeDistance);
                        // And also save the child by changing the state
                        child.GetComponent<ChildStateController>().currentState = ChildStateController.State.Follow;
                    }
                }
                else
                {
                    if (inventory.UseItem("Candy"))
                    {
                        childHealth.Heal(healAmouont);
                    }
                }
            }
        }
    }

    void UseRope()
    {
        GameObject child = GameObject.Find("Child");
        childTransform = child.GetComponent<Transform>();
        Vector2 playerPos = player.GetComponent<Transform>().position;
        Vector2 childPos = childTransform.position;
        ChildStateController.State curState = child.GetComponent<ChildStateController>().currentState;
        if (curState == ChildStateController.State.InTrap)
        {
            // Try to use a rope. If we have a rope.
            if (inventory.UseItem("Rope"))
            {
                Vector2 direction = (playerPos - childPos).normalized;
                child.transform.Translate(direction * freeDistance);
                // And also save the child by changing the state
                child.GetComponent<ChildStateController>().currentState = ChildStateController.State.Follow;
            }
        }
    }

    void UseCandy()
    {
        if (inventory.UseItem("Candy"))
        {
            childHealth.Heal(healAmouont);
        }
    }
}
