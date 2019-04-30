using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actionneur : Resettable
{
    public GameObject actionneurOn;
    public GameObject actionneurOff;
     
    private bool isOn = false;   //Triggered

    public bool resetAfterTime = false;
    public float timeBeforeReset = 10f;
    protected bool isActive = false;
    protected float timer;
    public Actionnable[] actionnables;
    protected GameObject objectDoingAction;

    public bool IsOn
    {
        get
        {
            return isOn;
        }

        set
        {
            isOn = value;
        }
    }

    public GameObject ObjectDoingAction
    {
        get
        {
            return objectDoingAction;
        }
    }

    protected void OpenActionnables()
    {
        if (!isActive)
        {
            foreach (Actionnable actionnable in actionnables)
            {
                actionnable.Open();
            }
            if (resetAfterTime)
            {
                isActive = true;
            }
        }
    }
    protected void CloseActionnables()
    {
        if (!isActive)
        {
            foreach (Actionnable actionnable in actionnables)
            {
                actionnable.Close();
            }
        }
    }

    protected void ToggleSprite()
    {
        if (!isActive)
        {
            if (IsOn)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = actionneurOn.GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = actionneurOff.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    public override void Reset()
    {
        isActive = false;
        IsOn = false;
        ToggleSprite();
    }

    public void ResetAfterTime()
    {
        isActive = false;
        timer = 0;
        IsOn = false;
        ToggleSprite();
    }
}
