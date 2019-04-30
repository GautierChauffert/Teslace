using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actionnable: Resettable
{
    public bool DefaultState;
    protected bool actualState;
    public abstract void Open();
    public abstract void Close();
}
