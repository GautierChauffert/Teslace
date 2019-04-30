using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewGravity : Actionnable {
    Rigidbody2D rigidbodyPlayer;

    // Use this for initialization
    void Start () {
        rigidbodyPlayer = gameObject.GetComponent<Rigidbody2D>();
    }

    public override void Close()
    {
        invertGravity();
    }

    public override void Open()
    {
        invertGravity();
    }

    private void invertGravity()
    {
        rigidbodyPlayer.gravityScale *= -1;
    }

    public override void Reset()
    {
        if (rigidbodyPlayer.gravityScale < 0)
            invertGravity();
    }
}
