using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDescription : MonoBehaviour
{
    public Text descriptions;
    const string defaultDes = "Go and find a battery to activate a level!\n";
    const string defaultTime = "Take your time... for now.\n";
    string desc;
    string time;
    string wholeText;

    // Start is called before the first frame update
    void Start()
    {
        desc = defaultDes;
        time = defaultTime;
        UpdateContents();
    }

    // Retrieve and update the mission descriptions;
    public void NewMission(string newDesc, float newMin, float newSec)
    {
        desc = newDesc;
        time = SetTime(newMin, newSec);
    }

    // Change time info to string;
    string SetTime (float min, float sec)
    {
        return min.ToString() + ":" + sec.ToString() + ":0";
    }

    // Update the contents displayed on panel;
    void UpdateContents()
    {
        wholeText = "Current Mission:\n\t" + desc + "Time Limit:\n\t" + time;
        if (descriptions != null)
        {
            descriptions.text = wholeText;
        }
    }
}
