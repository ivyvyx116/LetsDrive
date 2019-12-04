using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomMovement : MonoBehaviour
{
    public GameObject ActivateLevel;

    public string missionDescription;
    public float missionMinute;
    public float missionSeconds;

    public MissionDescription md;
    private void Start()
    {
        md = FindObjectOfType<MissionDescription>();
    }
    // activate mission when battery touched;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ActivateLevel.SetActive(true);
            // set canvas displays
            md.NewMission(missionDescription, missionMinute,missionSeconds);
            // set the description accordingly;
            // set time;
            // call timer and update
        }
    }
}