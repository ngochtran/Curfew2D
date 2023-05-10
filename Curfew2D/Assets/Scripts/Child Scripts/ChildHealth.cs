using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildHealth : MonoBehaviour
{

    [SerializeField]
    private int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 1)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Child Died");
    }
}
