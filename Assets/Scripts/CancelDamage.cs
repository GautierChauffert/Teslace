using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelDamage : Actionnable {
    public Damager damager;
    public override void Close()
    {

    }

    public override void Open()
    {
        damager.SetDamage(0);
    }

    public override void Reset()
    {

    }
}
