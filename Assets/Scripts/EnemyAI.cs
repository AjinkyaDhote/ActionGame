using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public GameObject lose;

    public float patrolSpeed = 0.1f;
    public float chaseSpeed =  0.15f;
    public float chaseWait = 3f;
    public float patrolWait = 1f;
    //public Transform[] patrolWayPoints;
    public List<Vector3> nodePoints;
    public GameObject gameManager;
    public GameObject enemy2D;

    private EnemySight enemySight;
    private NavMeshAgent nav;
    public Transform player;
    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex;
    private Animator anim;
    public float SpeedTime =14.07f;
    private float distanceTravelled;
    private int numberOfnodes;
    public GameObject map2D;
    public GameObject map3D;
    float width2DPlane, width3DPlane, height2DPlane, height3DPlane;
	bool initialized = false;

    public void InitializeEnemy()
    {
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        numberOfnodes = enemy2D.GetComponent<EnemyPatrol2D>().transformedPoints.Count;
        Vector3 size = map3D.GetComponent<MeshRenderer>().bounds.size;
        width3DPlane = size.x;
        height3DPlane = size.z;

        width2DPlane = map2D.GetComponent<SpriteRenderer>().sprite.textureRect.width;
        height2DPlane = map2D.GetComponent<SpriteRenderer>().sprite.textureRect.height;


        for(int numNodes =0; numNodes < numberOfnodes; numNodes++)
        {
            Vector2 temp = enemy2D.GetComponent<EnemyPatrol2D>().transformedPoints[numNodes];
            nodePoints.Add(convertPoint(temp));
        }

        transform.position = nodePoints[0];
        wayPointIndex = 1;

        anim = GetComponent<Animator>();
        anim.Play("idle", -1, 0.0f);
       
        for (int i = 0; i < numberOfnodes; i++)
        {
            if(i+1 != numberOfnodes)
            distanceTravelled += Vector3.Distance(nodePoints[i], nodePoints[i + 1]);
            else
            distanceTravelled += Vector3.Distance(nodePoints[i], nodePoints[0]);
        }
        patrolSpeed = (enemy2D.GetComponent<EnemyPatrol2D>().speed /width2DPlane) *width3DPlane * 100;
        anim.SetBool("wait", true);

		initialized = true;
    }


    Vector3 convertPoint(Vector2 relativePoint)
    {
        Vector3 returnVal;
        returnVal.x = (relativePoint.x / width2DPlane) * width3DPlane;
        returnVal.y = 0;
        returnVal.z = (relativePoint.y / height2DPlane) * height3DPlane;
        return returnVal;
    }



    void Update()
    {
		if (!gameManager.GetComponent<GameStartScript>().scene2D && initialized)
        {
            if (enemySight.playerInSight)
            {
                Chasing();
            }
            else
            {
                Patrolling();
            }
        }
    }

    void Chasing()
    {
        print("Chasing");
        Vector3 sightingDeltaPos = enemySight.LastSight - transform.position;
        if (sightingDeltaPos.sqrMagnitude > 4f)
            nav.destination = enemySight.LastSight;

        nav.speed = chaseSpeed;
        if (Vector3.Distance(this.transform.position,player.transform.position)< nav.stoppingDistance)
        {
            print("Bitch ");
            lose.SetActive( true );
            chaseTimer += Time.deltaTime;
            if (chaseTimer > chaseWait)
            {
                // enemySight.LastSight = Vector3(-100, -110, -100);
                chaseTimer = 0f;
            }
        }
        else
            chaseTimer = 0f;

    }

    void Patrolling()
    {

        nav.speed = patrolSpeed;
       
        //print("" + patrolTimer);
        if (patrolTimer > patrolWait)
        {
            if (wayPointIndex == nodePoints.Count - 1)
            {
                wayPointIndex = 0;
            }
            else
            {
                wayPointIndex++;
            }
            patrolTimer = 0f;
        }
        else
        {
            nav.destination = nodePoints[wayPointIndex];
        }

        if(Vector3.Distance(transform.position ,nav.destination) <0.16667f)
        {
            patrolTimer += Time.deltaTime;
            anim.SetBool("wait", true);
        }
        else
        {
            anim.SetBool("wait", false);
        }
    }
}
