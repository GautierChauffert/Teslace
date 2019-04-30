using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : Actionneur {
   private bool changed = false;
    private bool buttonPushed = false;
    private bool insideCollisionZone = false;
    private PlayerMovement playerMovement;

    private void Update()
    {
        if (playerMovement != null && playerMovement.ActionKeyPushed)
        {
            buttonPushed = true;
        }
        if (playerMovement != null && !playerMovement.ActionKeyPushed)
        {
            changed = false;
            buttonPushed = false;
        }
    }  

    private void FixedUpdate()
    {
        if (IsOn && buttonPushed && !changed && insideCollisionZone)
        {
            changed = true;
            IsOn = false;
            ToggleSprite();
            CloseActionnables();
            if (!isActive)
            {
                FindObjectOfType<AudioManager>().Play("Switch");
            }
        }
        else if (!IsOn && buttonPushed && !changed && insideCollisionZone)
        {
            IsOn = true;
            changed = true;
            ToggleSprite();
            OpenActionnables();
            if (!isActive)
            {
                FindObjectOfType<AudioManager>().Play("Switch");
            }
        }
        if(isActive){
            timer += Time.deltaTime;
            if(timer > timeBeforeReset)
            {
                ResetAfterTime();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("PlayerPhysic"))
            return;

        insideCollisionZone = true;
        playerMovement = col.GetComponentInParent<PlayerMovement>();
        objectDoingAction = col.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("PlayerPhysic"))
            return;
        insideCollisionZone = false;
        playerMovement = null;
        objectDoingAction = null;
    }
}
