using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    private Inventory inventory;
    private Bounds childBounds;
    private ChildStateController.State childState;
    private ChildHealth childHealth;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        GameObject child = GameObject.Find("Child");
        childBounds = child.GetComponent<Bounds>();
        childState = child.GetComponent<ChildStateController>().currentState;
        childHealth = child.GetComponent<ChildHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Use a rope if the child is stuck, otherwise use a candy
    }
}
