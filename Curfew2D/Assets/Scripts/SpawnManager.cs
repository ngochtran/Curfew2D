using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies = new GameObject[4];
    [SerializeField]
    private float minChildRadius = 15.0f;
    [SerializeField]
    private float maxChildRadius = 20.0f;
    [SerializeField]
    private float minPlayerRadius = 15.0f;
    [SerializeField]
    private float maxPLayerRadius = 20.0f;
    [SerializeField]
    private float minWaitTime = 10.0f;
    [SerializeField]
    private float maxWaitTime = 20.0f;

    private float childWaitTime;
    private float playerWaitTime;

    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        ResetChildTime();
        ResetPlayerTime();
        ResetEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        childWaitTime -= Time.deltaTime;
        playerWaitTime -= Time.deltaTime;

        if (childWaitTime < 0)
        {
            SpawnChild();
        }
        if (playerWaitTime < 0)
        {
            SpawnPlayer();
        }
    }

    void SpawnChild()
    {
        // Have to somehow avoid spawning in the camera
        float x = Random.Range(minChildRadius, maxChildRadius);
        float y = Random.Range(minChildRadius, maxChildRadius);

        int xSign = GetSign();
        int ySign = GetSign();

        Vector2 pos = new Vector2(x * xSign, y * ySign);
        Quaternion rot = new Quaternion();

        // Don't spawn if the location is in the camera bounds
        // Get the camera component
        Camera camera = Camera.main;

        // Get the position of the GameObject in viewport coordinates
        Vector3 viewportPos = camera.WorldToViewportPoint(gameObject.transform.position);

        // Check if the GameObject is within the camera's view frustum
        if (!(viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1 && viewportPos.z >= camera.nearClipPlane && viewportPos.z <= camera.farClipPlane))
        {
            // The GameObject is within the camera's view
            Instantiate(enemy, pos, rot);
        }
        ResetChildTime();

    }

    void SpawnPlayer()
    {
        float xCoord = Random.Range(minPlayerRadius, maxPLayerRadius);
        float yCoord = Random.Range(minPlayerRadius, maxPLayerRadius);

        int xSign = GetSign();
        int ySign = GetSign();

        Vector2 pos = new Vector2(xCoord * xSign, yCoord * ySign);
        Quaternion rot = new Quaternion();

        Instantiate(enemy, pos, rot);
        ResetPlayerTime();
    }

    void ResetChildTime()
    {
        childWaitTime = Random.Range(minWaitTime, maxWaitTime);
        ResetEnemy();
    }

    void ResetPlayerTime()
    {
        playerWaitTime = Random.Range(minWaitTime, maxWaitTime);
        ResetEnemy();
    }

    void ResetEnemy()
    {
        int i = Random.Range(0, enemies.Length);
        enemy = enemies[i];
    }

    int GetSign()
    {
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
