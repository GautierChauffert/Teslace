using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouch : MonoBehaviour {

    Rigidbody2D rigid;

    public LayerMask ground;

    bool isgrounded = false;

    public Actionnable[] actionnables;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isgrounded)
        {
            DoAction();
            isgrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if ((ground & 1 << theCollision.gameObject.layer) == 1 << theCollision.gameObject.layer)
            isgrounded = true;
    }

    void DoAction()
    {
        foreach (Actionnable actionnable in actionnables)
        {
            actionnable.Open();
        }
    }


}
