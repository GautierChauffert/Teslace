using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayStillOnPlatform : MonoBehaviour {
    public Transform parent;

    private void Start()
    {
        parent = this.transform.parent;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = parent;
        }
    }
}
