using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fieldOfView = 110f;
    public bool playerInSight;
    public Vector3 LastSight;

    private NavMeshAgent nav;
    private SphereCollider col;
   
    //private LastPlayerSighting lastPlayerSighting;
    public GameObject Player;
    //public Transform[] patrolWayPoints;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponentInChildren<SphereCollider>();
        Player = GameObject.FindGameObjectWithTag("Player");
       

    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == Player)
        {
            playerInSight = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject == Player)
        {
           // print("PlayerCaught COLLIDERD");
            //playerInSight = true;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            
            if(angle<fieldOfView*0.5f)
            {
                RaycastHit rayHit;
                if (Physics.Raycast(transform.position, direction, out rayHit))
                {
                    if(rayHit.collider.CompareTag("Player"))
                    {
                        playerInSight = true;
                        print("PlayerCaught");
                        LastSight = Player.transform.position;
                       
                    }
                }
            }
        }

    }
}
