using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDestroySelf : Actionnable
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Open()
    {
        Destroy(gameObject);
    }

    public override void Close()
    {

    }

    public override void Reset()
    {

    }
}
