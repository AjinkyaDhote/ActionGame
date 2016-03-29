using UnityEngine;
using System.Collections.Generic;

public class WaitButton : MonoBehaviour
{
    public GameObject map2D;
    public List<float> waitTimes;
    public bool wait;
    float[] waitArray = new float[100];
    int i;
    float timer;

    // Use this for initialization
    void Start()
    {
        wait = false;
        timer = 0;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (wait == true)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            //waitArray[i++] = timer;
            //timer = 0;
        }
        else if (wait == false)
        {

        }
    }


    void OnMouseDown()
    {
        if (wait == true)
        {
            wait = false;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            waitArray[i] = timer;
            waitTimes.Add(timer);
            Debug.Log(waitArray[i]);
            i++;
            timer = 0;
        }
        else if (wait == false)
        {
            wait = true;
            GetComponent<SpriteRenderer>().color = Color.gray;
            map2D.GetComponent<Map2DScript>().actionType.Add(2);
        }
    }
}
