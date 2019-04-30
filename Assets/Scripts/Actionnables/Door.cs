using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Actionnable
{

    Collider2D doorCollider;


    [SerializeField]
    GameObject DoorLocked;  //on

    [SerializeField]
    GameObject DoorUnlocked;  //off


    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = DoorLocked.GetComponent<SpriteRenderer>().sprite;

    }

    public override void Open()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DoorUnlocked.GetComponent<SpriteRenderer>().sprite;
    
        doorCollider.enabled = false;
    }

    public override void Close()
    {
    }

    public override void Reset()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DoorLocked.GetComponent<SpriteRenderer>().sprite;
        doorCollider.enabled = true;
    }
}
