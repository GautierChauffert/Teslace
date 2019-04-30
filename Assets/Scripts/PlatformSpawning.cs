using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawning : Actionnable {
    public float tempsApparition = 0.5f;
    float timer;
    bool isActive = false;

    public override void Close()
    {

    }

    public override void Open()
    {
        isActive = true;
        timer = tempsApparition;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void Reset()
    {
        
    }

    // Use this for initialization
    void Start () {
        timer = tempsApparition;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            isActive = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
	}
}
