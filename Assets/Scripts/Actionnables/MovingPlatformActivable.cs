using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformActivable : Actionnable
{
    public float PlatformSpeed = 3f;
    public bool isActivated;

    public Vector3[] points;

    private bool forceToNextStop;
    private bool defaultIsActivated; // Retiens la valeur initiale du script
    private int targetedPointIndex;

	void Start () {
        defaultIsActivated = isActivated;
        if (gameObject.transform.position != points[targetedPointIndex])
            forceToNextStop = true;
        
    }
	
	void Update () {
        gameObject.transform.position = Vector3.MoveTowards(
            gameObject.transform.position,
            points[targetedPointIndex],
            Time.deltaTime * ((isActivated || forceToNextStop)? PlatformSpeed : 0f)
        );

        if (gameObject.transform.position == points[targetedPointIndex])
        {
            forceToNextStop = false;
            if((targetedPointIndex + 1) == points.Length)
            {
                targetedPointIndex = 0;
            }
            else
            {
                targetedPointIndex++;
            }         
        }
    }

    public override void Open()
    {
        isActivated = true;
        FindObjectOfType<AudioManager>().Play("MovingPlatform");
    }


    public override void Close()
    {
        isActivated = false;
        FindObjectOfType<AudioManager>().Stop("MovingPlatform");
    }

    public override void Reset()
    {
        gameObject.transform.position = points[0];
        isActivated = defaultIsActivated;
    }
}
