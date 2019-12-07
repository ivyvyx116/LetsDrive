using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HulkController : MonoBehaviour
{
    public Transform[] waypointArray;
    public int currentIndex;
    Transform targetWaypoint;
    Collider PC;
    Rigidbody RB;

    private int count;

    public float speed = 4;
    public float animationTimer = 0;
    public float animationFPS = 5;
    private int currentFrame = 0;

    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        PC = gameObject.GetComponent<Collider>();
        RB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex < waypointArray.Length)
        {
            if (targetWaypoint == null)
            {
                targetWaypoint = waypointArray[currentIndex];
            }
            walk();
        }
        if (!dead)
        {
           
        }

    }

    void walk()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
        if (transform.position == targetWaypoint.position)
        {
            currentIndex++;

            if (currentIndex >= waypointArray.Length)
            {
                currentIndex = 0;
            }
            targetWaypoint = waypointArray[currentIndex];

            turnHulk();
        }
    }

    void turnHulk()
    {
        transform.right = (targetWaypoint.position - transform.position).normalized;
    }


    public void killer()
    {
        dead = true;
    }
}