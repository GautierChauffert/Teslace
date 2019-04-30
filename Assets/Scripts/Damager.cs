using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : Resettable {
    public int defaultDamage = 1;
    private int damage;
    private int collisionStayCount = 0;
    private int collisionEnterCount = 0;


    void Start()
    {
        this.damage = this.defaultDamage;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        collisionStayCount++;
        DamagePlayer(collision.gameObject);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        collisionEnterCount++;
        DamagePlayer(collision.gameObject);
    }

    void DamagePlayer(GameObject gameObject)
    {
        Health injuredObjectHealth = gameObject.GetComponent<Health>();
        if (injuredObjectHealth != null)
        {
            injuredObjectHealth.IsHit(damage);

        }
    }

    public void SetDamage(int newDamagePoints)
    {
        this.damage = newDamagePoints;
    }

    public override void Reset()
    {
        Start();
    }

}
