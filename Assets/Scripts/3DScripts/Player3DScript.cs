using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player3DScript : MonoBehaviour
{
    public GameObject map2D;
    public GameObject map3D;
    public GameObject waitButton;
    public GameObject player2D;

    public GameObject win;
    public GameObject diamondLocation;

    Animator anim;
    Vector3 source;
    float speed;
    Vector3 target;
	List<Vector2> mapPoints;

    List<float> waitTimes;
    List<int> actionTypes;

    enum WaitState { WaitInProgress, WaitCompleted };

    int currentAction;
    int currentDestination;

    int currentWaitIndex;
    float currentWaitTime;


    bool doNothing = false;
    float epsilon = 0.5f;
    WaitState currentWaitState = WaitState.WaitCompleted;


    float width2DPlane, width3DPlane, height2DPlane, height3DPlane;

    void Start()
    {
        doNothing = true;
        anim = GetComponent<Animator>();
	}

    Vector3 convertPoint(Vector2 relativePoint)
    {
        Vector3 returnVal;
        returnVal.x = (relativePoint.x / width2DPlane) * width3DPlane;
        returnVal.y = 0;
        returnVal.z = (relativePoint.y / height2DPlane) * height3DPlane;
        return returnVal;
    }

    public void InitializePlayer()
    {
        mapPoints = map2D.GetComponent<Map2DScript>().mapPoints;
        actionTypes = map2D.GetComponent<Map2DScript>().actionType;
        waitTimes = waitButton.GetComponent<WaitButton>().waitTimes;

		Vector3 size = map3D.GetComponent<MeshRenderer>().bounds.size;
        width3DPlane = size.x;
        height3DPlane = size.z;

        width2DPlane = map2D.GetComponent<SpriteRenderer>().sprite.textureRect.width;
        height2DPlane = map2D.GetComponent<SpriteRenderer>().sprite.textureRect.height;

        source = convertPoint(mapPoints[0]);
        if ( mapPoints.Count == 1 )
        {
            target = convertPoint(mapPoints[0]);
        }
        else
        {
            target = convertPoint(mapPoints[1]);
        }
        transform.position = source;
        currentDestination = 1;
        currentAction = 0;
        currentWaitIndex = 0;

        speed = (player2D.GetComponent<Player2DScript>().speed / width2DPlane) * width3DPlane*100;
        doNothing = false;
    }


    void Update()
    {
        if ( Vector3.Distance(transform.position, diamondLocation.GetComponent<Transform>().position) <1.5 )
        {
            win.SetActive(true);
        }
        if (!doNothing)
        {
            // movement 
            if (actionTypes[currentAction] == 1)
            {
                float distanceLeft = Vector3.Distance(transform.position, target);

                if (distanceLeft < epsilon)
                {
                    currentDestination++;
                    currentAction++;

                    if (currentAction == actionTypes.Count)
                    {
                        doNothing = true;
                    }

                    if (!doNothing && currentDestination < mapPoints.Count)
                    {
                        source = target;
                        target = convertPoint(mapPoints[currentDestination]);
                    }
                }

                if (!doNothing && actionTypes[currentAction] == 1)

                {
                    Vector3 direction = (target - source).normalized;
                    transform.Translate((direction) * Time.deltaTime * speed, Space.World);
                    anim.SetBool("Idle", false);
                }
            }
            else if (actionTypes[currentAction] == 2)
            {
                // waiting
                if (currentWaitState == WaitState.WaitCompleted)
                {
                    currentWaitTime = waitTimes[currentWaitIndex];
                    currentWaitState = WaitState.WaitInProgress;
                }
                else if ( currentWaitState == WaitState.WaitInProgress )
                {
                    Debug.Log("Stopping here");
                    anim.SetBool("Idle", true);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Debug.Log("Shooting Idle is playing");
                        anim.SetTrigger("ShootingIdle");
                    }

                    currentWaitTime -= Time.deltaTime;

                    if ( currentWaitTime < 0 )
                    {
                        currentWaitIndex++;
                        currentAction++;
                        currentWaitState = WaitState.WaitCompleted;

                        if (currentAction == actionTypes.Count)
                        {
                            doNothing = true;
                        }
                    }
                }
            }
        }

    }
}

