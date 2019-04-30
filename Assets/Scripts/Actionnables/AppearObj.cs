using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearObj : Actionnable
{
    public GameObject ObjectToAppear;  //on

    public bool appearOnce = true;

    bool appeared = false;
    List<GameObject> createdObjects = new List<GameObject>();
    
    public override void Open()
    {
        if (!appeared || (appeared && !appearOnce))
        {
            createdObjects.Add(Instantiate(ObjectToAppear, gameObject.transform));
            appeared = true;
        }
    }


    public override void Close()
    {

    }

    public override void Reset()
    {
        appeared = false;
        createdObjects.ForEach(
            createdGameObject => Destroy(createdGameObject)
        );
    }
}
