using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDespawner : MonoBehaviour
{
    [SerializeField]
    private float despawnRadius = 10.0f;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = playerTransform.position;
        if (Vector2.Distance(transform.position, playerPos) > despawnRadius)
        {
            Destroy(gameObject);
        }
    }
}
