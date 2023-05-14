using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float startingTime = 0;
    public TMP_Text printedScore;
    // Start is called before the first frame update
    void Start()
    {
        if (TimeChanger.score == 'S')
        {
            printedScore.text = "Score: S";
        }
        else if (TimeChanger.score == 'A')
        {
            printedScore.text = "Score: A";
        }
        else if (TimeChanger.score == 'B')
        {
            printedScore.text = "Score: B";
        }
        else if (TimeChanger.score == 'C')
        {
            printedScore.text = "Score: C";
        }
        else if (TimeChanger.score == 'F')
        {
            printedScore.text = "Score: F";
        }
    }
}
