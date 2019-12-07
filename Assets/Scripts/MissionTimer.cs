using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionTimer : MonoBehaviour
{
    public Text timer;
    public int minutes = 5;
    public int seconds = 0;
    private float miliseconds = 0;
    public bool isTiming;
    public int levelCount;
    public GameObject car;

    private void Start()
    {
        Roam();
    }

    void Update()
    {
        if (minutes < 0)
        {
            isTiming = false;
            TimeUp();
        }

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
    }

    public void BeginTiming(int newMin, int newSec)
    {
        minutes = newMin;
        seconds = newSec;
        isTiming = true;
    }

    public void TimeUp()
    {
        timer.color = Color.black;
        timer.text = "Time's Up. Try Again.";
        isTiming = false;
        car.GetComponent<CarController>().resetTrack();
        // deactivate level, activate battery;
    }

    public void Win()
    {
        if (levelCount <= 1)
        {
            // Load Win Scene
            SceneManager.LoadScene(2);
        }
        else
        {
            timer.color = Color.black;
            timer.text = "You Win This Round!\nGo Find Another Battery!";
            timer.color = Color.black;
            isTiming = false;
            if (car.GetComponent<CarController>().battery.GetComponent<MeshRenderer>().material.color != Color.green)
            {
                levelCount--;
            }
            car.GetComponent<CarController>().resetTrack();
            car.GetComponent<CarController>().battery.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
        }
    }

    void Roam()
    {
        timer.text = "Level Roaming Time";
        isTiming = false;
    }
}
