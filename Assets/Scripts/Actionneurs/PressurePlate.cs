using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Actionneur {

    void OnTriggerEnter2D(Collider2D col)
    {
        IsOn = true;
        ToggleSprite();
        FindObjectOfType<AudioManager>().Play("PressionPressed");
        objectDoingAction = col.gameObject;
        OpenActionnables();
    }

    private void OnTriggerExit2D(Collider2D col)
    {     
        IsOn = false;
        ToggleSprite();
        FindObjectOfType<AudioManager>().Play("PressionUnpressed");
        CloseActionnables();
        if (isActive)
        {
            ResetAfterTime();
        }
    }
}
