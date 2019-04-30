using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMultiActivation : Actionnable
{
    public float moveSpeed = 3f;
    public Vector3[] points;

    private int position = 0;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, points[position], Time.deltaTime * moveSpeed);
    }

    public override void Open()
    {
        if ((position + 1) == points.Length)
            position = 0;
        else
            position++;
    }

    public override void Close()
    {
       
    }

    public override void Reset()
    {
        position = 0;
    }
}
