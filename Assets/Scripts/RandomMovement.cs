using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomMovement : MonoBehaviour
{
    public GameObject ActivateLevel;

    public string missionDescription;
    public int missionMinute;
    public int missionSeconds;

    public MissionDescription md;
    public MissionTimer mt;
    public int objectsToCollect;

    private void Start()
    {
        md = FindObjectOfType<MissionDescription>();
        mt = FindObjectOfType<MissionTimer>();
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
            mt.BeginTiming(missionMinute, missionSeconds);
            // call timer and update
        }
    }
}