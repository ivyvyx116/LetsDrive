using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HulkController : MonoBehaviour
{
    public Transform[] waypointArray;
    public Sprite idle;
    public Sprite[] walking;
    public Sprite[] explode;
    public Sprite dying;
    public int currentIndex;
    Transform targetWaypoint;
    SpriteRenderer SR;
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
        SR = gameObject.GetComponent<SpriteRenderer>();
        PC = gameObject.GetComponent<Collider>();
        RB = gameObject.GetComponent<Rigidbody>();
        count = explode.Length;
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
            PlayBackAnimation(walking);
        }
             
    }

    void walk()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
        if(transform.position == targetWaypoint.position)
        {
            currentIndex++;

            if(currentIndex >= waypointArray.Length)
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

    void PlayBackAnimation(Sprite[] anim)
    {
        animationTimer -= Time.deltaTime;
        if(animationTimer <= 0 && anim.Length > 0)
        {
            animationTimer = 1f / animationFPS;
            currentFrame++;
            if(currentFrame >= anim.Length)
            {
                currentFrame = 0;
            }
            SR.sprite = anim[currentFrame];
        }
    }

    public void killer()
    {
        StartCoroutine(getKilled(explode));
        dead = true;
    }

    IEnumerator getKilled(Sprite[] die)
    {
        SR.sprite = dying;
        yield return new WaitForSeconds((float)0.75);
        PC.enabled = false;

        for(int i = 0; i < count; i++)
        {
            SR.sprite = die[i];
            yield return new WaitForSeconds((float)0.075);
        }
        Destroy(gameObject);
    }
}
