using UnityEngine;
using System.Collections.Generic;

public class Map2DScript : MonoBehaviour
{

    public Vector3 target;
    public Transform PlayerShadowPrefab;
    public Transform LinePrefab;
    public Queue<Vector3> targetQueue = new Queue<Vector3>();
    public List<Vector2> mapPoints;
    public List<int> actionType;

    LineRenderer LineR;
    Transform line;
    Vector3 pos;
    Vector3 Source;
    float unitsToPixels;
    Vector3[] positionArray = new Vector3[100];
    int count;

    void Start()
    {
        mapPoints.Clear();

        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        unitsToPixels = sp.sprite.pixelsPerUnit;
        count = 0;
        Source = new Vector3(-0.015f, -33.967f, 0);
        positionArray[count] = Source;

        Vector3 sourcePixelPos = transform.InverseTransformPoint(Source);
        Vector2 tranFormed;
        tranFormed.x = sourcePixelPos.x * unitsToPixels;
        tranFormed.y = sourcePixelPos.y * unitsToPixels;
        mapPoints.Add(tranFormed);

        targetQueue.Enqueue(Source);

        Instantiate(PlayerShadowPrefab, Source, Quaternion.identity);
    }

    void Update()
    {
    }

    void OnMouseDown()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = transform.position.z - 0.01f;
        target = pos;
        count++;
        positionArray[count] = target;
        targetQueue.Enqueue(target);

        Instantiate(PlayerShadowPrefab, target, Quaternion.identity);
        line = Instantiate(LinePrefab, Source, Quaternion.identity) as Transform;
        LineR = line.GetComponent<LineRenderer>();
        LineR.SetPosition(0, Source);
        LineR.SetPosition(1, target);

        Source = target;
        Vector3 sourcePixelPos = transform.InverseTransformPoint(Source);
        Vector2 tranFormed;
        tranFormed.x = sourcePixelPos.x * unitsToPixels;
        tranFormed.y = sourcePixelPos.y * unitsToPixels;
        mapPoints.Add(tranFormed);

        actionType.Add(1);
    }
}
