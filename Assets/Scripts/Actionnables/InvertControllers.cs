using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InvertControllers : Actionnable {
    public GameObject[] playersPhysicToSwap;
    public GameObject[] players;
    private int offset;

    void Start()
    {
        players = playersPhysicToSwap.Select(g => g.transform.parent.gameObject).ToArray();
    }

    public override void Close()
    {
        //Change();
    }

    public override void Open()
    {
        Change();
    }

    public override void Reset()
    {
        offset = -1;
        Change();
    }

    private void Change()
    {
        offset++;
        for(int i = 0; i < playersPhysicToSwap.Length; i++)
        {
            players[(offset + i) % players.Length].GetComponent<PlayerMovement>().CharacterController = playersPhysicToSwap[i].GetComponent<CharacterController2D>();
        }
    }
}
