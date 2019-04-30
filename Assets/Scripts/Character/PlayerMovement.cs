using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Resettable
{
    private CharacterController2D characterController;
    public float runSpeed = 40f;
    public int playerNumber = 1;
    public ControllerType controllerType = ControllerType.Keyboard;

    float horizontalMove = 0f;
    bool jumping = false;
    bool cruching = false;
    bool action = false;

    // Use this for initialization
    void Start()
    {
        if(CharacterController == null)
            CharacterController = gameObject.GetComponent<CharacterController2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw(PlayerKey("Horizontal")) * runSpeed;
        if (Input.GetButtonDown(PlayerKey("Jump")))
            jumping = true;
        else if (Input.GetButtonUp(PlayerKey("Jump")))
            jumping = false;
        if (Input.GetButtonDown(PlayerKey("Action")))
            action = true;
        else if (Input.GetButtonUp(PlayerKey("Action")))
            action = false;
    }
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (CharacterController != null)
            CharacterController.Move(horizontalMove, cruching, jumping);
    }

    public void SetPlayerNumber(int newPlayerNumber)
    {
        // Si le joueur actuel est appuyé sur une touche alors on annule
        Reset();
        playerNumber = newPlayerNumber;
    }

    internal string PlayerKey(string origin)
    {
        return string.Concat("J", playerNumber, controllerType.ToString(), origin);
    }

    public bool ActionKeyPushed
    {
        get
        {
            return action;
        }

        set
        {
            action = value;
        }
    }

    public CharacterController2D CharacterController
    {
        get
        {
            return characterController;
        }

        set
        {
            characterController = value;
            Reset();
        }
    }

    public override void Reset()
    {
        jumping = false;
        cruching = false;
    }
}

[SerializeField]
public enum ControllerType
{
    Keyboard, Controller
}