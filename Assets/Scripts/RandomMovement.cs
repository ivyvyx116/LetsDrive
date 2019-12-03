using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomMovement : MonoBehaviour
{
    private Vector3 tyreWorldPosition;
    private Vector3 rotation;
    public GameObject ActivateLevel;

    public string missionDescription;
    public float missionMinute;
    public float missionSeconds;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    /*
    public void loadMission()
    {
        print("Start Mission");
    }

    IEnumerator activateTyres()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            //Instantiate(obstacle, transform.position, transform.rotation);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            print("start mission");
            ActivateLevel.SetActive(true);
        }
    }
}