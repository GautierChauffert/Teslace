using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearingPlatform : Resettable {
    bool touched = false;
    public float timer = 0.5f;
    float disapearIn;
    SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        disapearIn = timer;
	}
	
	// Update is called once per frame
	void Update () {
        if (touched)
        {
            disapearIn -= Time.deltaTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, (disapearIn/timer));
        }
        if (disapearIn < 0)
        {
            GetComponent<Collider2D>().enabled = false;
            spriteRenderer.enabled = false;
            
        }
    }

    void OnCollisionEnter2D(Collision2D theCollision)
    {
        touched = true;
    }

    public override void Reset()
    {
        touched = false;
        disapearIn = timer;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

    }
}
