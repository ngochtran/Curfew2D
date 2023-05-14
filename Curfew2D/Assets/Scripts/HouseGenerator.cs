using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] points;

    [SerializeField]
    private GameObject house;

    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, points.Length);
        GameObject point = points[i];
        Vector3 pos = new Vector3(point.transform.position.x, point.transform.position.y, 2.0f);
        Instantiate(house, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
