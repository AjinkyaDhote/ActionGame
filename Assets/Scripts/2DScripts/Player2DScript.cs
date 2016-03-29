using UnityEngine;
using System.Collections;



public class Player2DScript : MonoBehaviour
{

    public GameObject Target;
    public bool Paused;
    Vector3 Source;
    Vector3 Destination;
    public float speed;

    void Start()
    {
        Source = transform.position;
        Destination = transform.position;
        speed = 2f;
    }

    void FixedUpdate()
    {
        Vector3 Direction = (Destination - Source).normalized;
        float Distance = Vector3.Distance(transform.position, Destination);
        if (Distance > 0.1f)
        {
            transform.Translate((Direction * (speed * Time.deltaTime)));
            Paused = false;
        }
        else
        {
            Paused = true;
            transform.position = Destination;
            Source = transform.position;
            if (Target.GetComponent<Map2DScript>().targetQueue.Count != 0)
                Destination = Target.GetComponent<Map2DScript>().targetQueue.Dequeue();
        }
    }
}