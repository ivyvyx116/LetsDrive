using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyCapsule : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("battery"))
        {
            Destroy(other.gameObject);
        }   
    }
}
