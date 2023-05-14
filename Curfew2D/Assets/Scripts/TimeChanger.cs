using UnityEngine;
using TMPro;

public class TimeChanger : MonoBehaviour
{
    private float startingTime = 0;
    private int currentTime;
    public TMP_Text time;
    public TMP_Text PM;
    public static char score;
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
        if (timeElapsed >= 25 && timeElapsed < 50)
        {
            currentTime = 6;
            score = 'S';
        }
        if (timeElapsed >= 50 && timeElapsed < 75)
        {
            currentTime = 7;
            score = 'S';
        }
        if (timeElapsed >= 75 && timeElapsed < 100)
        {
            currentTime = 8;
            score = 'A';
        }
        if (timeElapsed >= 100 && timeElapsed < 125)
        {
            currentTime = 9;
            score = 'A';
        }
        if (timeElapsed >= 150 && timeElapsed < 175)
        {
            currentTime = 10;
            score = 'B';
        }
        if (timeElapsed >= 175 && timeElapsed < 200)
        {
            currentTime = 11;
            score = 'B';
        }
        if (timeElapsed >= 200)
        {
            currentTime = 12;
            score = 'C';
        }
        if (timeElapsed <= 200)
        {
            time.text = currentTime.ToString();
        } else {
            score = 'F';
            time.text = "OT";
            PM.text = "";
        }

    }
}
