using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTimer : MonoBehaviour
{
    public Text timer;
    static int minutes = 5;
    static int seconds = 0;
    private int miliseconds = 0;
    private bool isTiming;

    private void Start()
    {
        isTiming = false;
    }

    void Update()
    {
        if (isTiming)
        {
            if (miliseconds <= 0)
            {
                if (seconds <= 0)
                {
                    minutes--;
                    seconds = 59;
                }
                else if (seconds >= 0)
                {
                    seconds--;
                }
                miliseconds = 100;
            }
            miliseconds -= (int)Time.deltaTime * 100;
        }
        timer.text = string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds);

        // Timer color change
        if (minutes <= 0 && seconds <= 15)
        {
            timer.color = Color.red; // set color to red under 15s
        }
        else
        {
            timer.color = Color.black;
        }

        // state mission failed
        // deactivate level, activate battery;
        // stop timing
        if (minutes == 0 && seconds == 0 && miliseconds == 0)
        {
            isTiming = false;
        }
    }

    static void BeginTiming()
    {
        //set numbers
        // bool = true
    }
}
