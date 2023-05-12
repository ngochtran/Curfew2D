using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private Dictionary<string, int> items;

    // Start is called before the first frame update
    void Start()
    {
        items = new Dictionary<string, int>();
        items.Add("Rope", 0);
        items.Add("Candy", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(string type)
    {
        // We can do some dictionary type data structure here
        items[type] += 1;
    }

    public bool UseItem(string type)
    {
        if (items[type] > 0)
        {
            items[type] -= 1;
            return true;
        }
        return false;
    }
}
