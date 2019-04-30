using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMultiActivation : Actionnable
{
    Collider2D doorCollider;
    bool ouverte;

    [SerializeField]
    GameObject DoorLocked;  //on

    [SerializeField]
    GameObject DoorUnlocked;  //off

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = DoorLocked.GetComponent<SpriteRenderer>().sprite;
        ouverte = false;
    }

    public override void Open()
    {
        if (!ouverte)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = DoorUnlocked.GetComponent<SpriteRenderer>().sprite;
            doorCollider.enabled = false;
            ouverte = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = DoorLocked.GetComponent<SpriteRenderer>().sprite;
            doorCollider.enabled = true;
            ouverte = false;
        }
    }

    public override void Close()
    {
        if(!ouverte)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = DoorUnlocked.GetComponent<SpriteRenderer>().sprite;
            doorCollider.enabled = false;
            ouverte = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = DoorLocked.GetComponent<SpriteRenderer>().sprite;
            doorCollider.enabled = true;
            ouverte = false;
        }
    }

    public override void Reset()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DoorLocked.GetComponent<SpriteRenderer>().sprite;
        doorCollider.enabled = true;
        ouverte = false;
    }
}
