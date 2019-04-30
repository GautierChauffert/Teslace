using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturDoor : Actionnable
{

    Animator anim;
    Collider2D collider1;    

    [SerializeField]
    GameObject FuturDoor1;  //on


    void Start()
    {
        anim = GetComponent<Animator>();
        collider1 = GetComponent<Collider2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = FuturDoor1.GetComponent<SpriteRenderer>().sprite;
        actualState = DefaultState;
        if (DefaultState)
            OpenDoor();
    }

    public override void Open()
    {
        Toggle();
    }

    public override void Close()
    {
        Toggle();
    }
    public void Toggle()
    {
        if(!actualState)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        collider1.enabled = false;
        actualState = true;
        anim.Play("DoorOpening");
        FindObjectOfType<AudioManager>().Play("DoorOpening");
    }
    private void CloseDoor()
    {
        collider1.enabled = true;
        actualState = false;
        anim.Play("DoorClosing");
        FindObjectOfType<AudioManager>().Play("DoorClosing");
    }

    public override void Reset()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = FuturDoor1.GetComponent<SpriteRenderer>().sprite;
        if (DefaultState)
            OpenDoor();
        else
            CloseDoor();
    }


}
