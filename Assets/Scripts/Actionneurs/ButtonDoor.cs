using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : Actionneur {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsOn)
        {
            objectDoingAction = collision.gameObject;
            IsOn = true;
            FindObjectOfType<AudioManager>().Play("ButtonClicked");
            ToggleSprite();
            OpenActionnables();
        }    
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer > timeBeforeReset)
            {
                ResetAfterTime();
            }
        }
    }
}
