using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformByImpulse : Actionnable
{
    public float PlatformSpeedDown = 5f;
    public float PlatformSpeedUp = 2f;
    public Vector3 pointInitial;
    public Vector3 pointMax;
    private float timer = 0f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pointMax, Time.deltaTime * PlatformSpeedDown);
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pointInitial, Time.deltaTime * PlatformSpeedUp);
        }
    }

    public override void Open()
    {
        PlateformeDown();
    }

    public override void Close()
    {
        PlateformeDown();
    }

    public override void Reset()
    {
        gameObject.transform.position = pointInitial;
    }

    private void PlateformeDown()
    {
        if (gameObject.transform.position != pointMax)
        {
            timer = 0.1f;
        }
    }
}
