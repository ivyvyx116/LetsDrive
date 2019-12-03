using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTimer : MonoBehaviour
{
    public Text timer;
    public float minutes = 5;
    public float seconds = 0;
    public float miliseconds = 0;

    void Update()
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

        if (minutes <= 0 && seconds <= 15)
        {
            timer.color = Color.red;
        } else
        {
            timer.color = Color.black;
        }

        miliseconds -= Time.deltaTime * 100;
        timer.text = string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds);
    }
}
