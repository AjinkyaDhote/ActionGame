using UnityEngine;
using System.Collections.Generic;

public class EnemyPatrol2D : MonoBehaviour
{

    public Vector3[] PatrolBetween;
    public float speed;
    public GameObject player;
    public GameObject Wait;
    public Transform killed;
    public GameObject map2D;
    Vector3 Source;
    Vector3 Destination;
    int index;
    bool paused;
    bool wait;
    public float wait_time;
    public float PatrolWait;
    //MoveTo moveTo;
    public List<Vector2> transformedPoints;

    // Use this for initialization
    void Start()
    {
        Source = PatrolBetween[0];
        transform.position = Source;
        Destination = PatrolBetween[1];
        PatrolWait = 1.0f;

        //speed = 5f;
        index = 1;

        SpriteRenderer sp = map2D.GetComponent<SpriteRenderer>();
        float unitsToPixels = sp.sprite.pixelsPerUnit;

        for ( int k=0; k<PatrolBetween.Length; k++ )
        {
            Vector3 sourcePixelPos = map2D.transform.InverseTransformPoint(PatrolBetween[k]);
            Vector2 tranFormed;
            tranFormed.x = sourcePixelPos.x * unitsToPixels;
            tranFormed.y = sourcePixelPos.y * unitsToPixels;
            transformedPoints.Add(tranFormed);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        paused = player.GetComponent<Player2DScript>().Paused;
        wait = Wait.GetComponent<WaitButton>().wait;
        //Debug.Log(paused + " " + wait);
        if (!paused || wait)
        {
            Vector3 Direction = (Destination - Source).normalized;
            float Distance = Vector3.Distance(transform.position, Destination);
            if (Distance > 0.1f)
            {
                transform.Translate((Direction * (speed * Time.deltaTime)));
            }
            else
            {
                wait_time += Time.deltaTime;
                if (PatrolWait < wait_time)
                {
                    //Debug.Log(count++);
                    transform.position = Destination;
                    Source = transform.position;
                    // if(i<4)
                    Destination = PatrolBetween[(++index) % PatrolBetween.Length];
                    wait_time = 0f;
                }
            }
           
        }
    }

    void OnMouseDown()
    {
        Instantiate(killed, transform.position, Quaternion.identity);
        gameObject.SetActive(false);

    }
}
