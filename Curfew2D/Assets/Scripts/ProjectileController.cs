using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float speed = 10.0f;

    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        // Get the right direction by finding the mouse location and comparing it to our current location
        Vector2 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 curLocation = transform.position;
        direction = (mouseLocation - curLocation).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile in the given direction
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
        }
    }
}
