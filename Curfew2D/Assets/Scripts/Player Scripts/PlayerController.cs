using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    public float attackCooldown = 1.0f;

    private float horizontalInput;
    private float verticalInput;
    private float currentCooldown = 0.0f;

    private Inventory inventory;

    Vector2 movement;

    public GameObject projectile;

    // Animation Variables 
    public Animator animator; 

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the controller
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // And move our character in the world. The bounding is automatically handled by KeepActorInBounds
        transform.Translate(Vector2.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector2.up * speed * verticalInput * Time.deltaTime);

        animator.SetFloat("Horizontal", horizontalInput);
        animator.SetFloat("Vertical", verticalInput);

        // Spawn a projectile if we press the space bar and our cooldown is done
        if (Input.GetKeyDown(KeyCode.Space) && currentCooldown <= 0)
        {
            SpawnProjectile();
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    void SpawnProjectile()
    {
        currentCooldown = attackCooldown;
        Instantiate(projectile, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            // See if we have a rope or a candy and add it to our inventory
            if (collision.GetComponent<RopeController>() != null)
            {
                inventory.AddItem("Rope");
                Destroy(collision.gameObject);
            }
            else if (collision.GetComponent<CandyController>() != null)
            {
                inventory.AddItem("Candy");
                Destroy(collision.gameObject);
            }
        }
    }
}
