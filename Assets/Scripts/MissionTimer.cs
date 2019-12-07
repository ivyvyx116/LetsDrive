using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTimer : MonoBehaviour
{
    public Text timer;
    int minutes = 5;
    int seconds = 0;
    private float miliseconds = 0;
    private bool isTiming;

    private void Start()
    {
        timer.text = "Level Roaming Time";
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

            miliseconds -= Time.deltaTime * 100;

            timer.text = string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds);
        }
        

        // Timer color change
        if (minutes <= 0 && seconds <= 15)
        {
            timer.color = Color.red; // set color to red under 15s
        }
        else
        {
            timer.color = Color.black;
        }

        if (minutes == 0 && seconds == 0 && System.Math.Abs(miliseconds) < float.Epsilon)
        {
            isTiming = false;
            TimeUp();
        }
    }

    public void BeginTiming(int newMin, int newSec)
    {
        minutes = newMin;
        seconds = newSec;
        isTiming = true;
    }

    void TimeUp()
    {
        timer.text = "Time's Up. Try Again.";
        // deactivate level, activate battery;
    }
}
