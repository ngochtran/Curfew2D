using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class TimeChanger : MonoBehaviour
{
    private float startingTime = 0;
    private int currentTime;
    public TMP_Text time;
    public TMP_Text PM;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 5;
    }

    private void Awake()
    {
        startingTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var timeElapsed = Time.time - startingTime;
        print(timeElapsed);
        if (timeElapsed >= 25 && timeElapsed < 50)
        {
            currentTime = 6;
        }
        if (timeElapsed >= 50 && timeElapsed < 75)
        {
            currentTime = 7;
        }
        if (timeElapsed >= 75 && timeElapsed < 100)
        {
            currentTime = 8;
        }
        if (timeElapsed >= 100 && timeElapsed < 125)
        {
            currentTime = 9;
        }
        if (timeElapsed >= 150 && timeElapsed < 175)
        {
            currentTime = 10;
        }
        if (timeElapsed >= 175 && timeElapsed < 200)
        {
            currentTime = 11;
        }
        if (timeElapsed >= 200)
        {
            currentTime = 12;
        }
        if (timeElapsed <= 200)
        {
            time.text = currentTime.ToString();
        } else {
            time.text = "OT";
            PM.text = "";
        }

    }
}
