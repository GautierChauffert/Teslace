using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Resettable
{
    public int maxLife = 5;
    public bool godMode = false;
    public bool deathIfNotVivible = true;
    private int life = 5;
    private int deathCount = 0;
    private CharacterController2D characterController;
    private CheckpointManager checkpointManager;
    private int damageLevel = 0;


    public int LifeLeft {
        get
        {
            return (int)(life * 100 / maxLife);
        }
    }

    public int DeathCount
    {
        get
        {
            return deathCount;
        }

        set
        {
            deathCount = value;
        }
    }

    public int DamageLevel
    {
        get
        {
            return damageLevel;
        }

        set
        {
            damageLevel = value;
        }
    }

    void Awake()
    {
        characterController = gameObject.GetComponent<CharacterController2D>();
        checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
    }

    void Start()
    {
        this.life = this.maxLife;
    }

    public void IsHit(int damage)
    {
        if (checkpointManager.IsResseting)
            return;

        if (!godMode)
            life -= damage;
        if (IsDead())
        {
            Debug.Log("Mort N°" + DeathCount + " de " + gameObject.name);
            DeathCount++;
            DeathEffect();
            checkpointManager.GetPlayersBackToLastCheckpoint();
            
        }
    }

    public override void Reset()
    {
        this.life = this.maxLife;
    }

    internal bool IsDead()
    {
        return (this.life <= 0);
    }

    void OnBecameInvisible()
    {
        if (!checkpointManager.IsResseting)
            IsHit(999);
    }

    private void DeathEffect()
    {
        if (DeathCount > 5 && damageLevel<1)
        {
            damageLevel = 1;
            characterController.JumpForce -= 10f;
        }
        if (DeathCount > 10 && damageLevel < 2)
        {
            damageLevel = 2;
            characterController.JumpForce -= 10f;
        }
        if (DeathCount > 15 && damageLevel < 3)
        {
            damageLevel = 3;
            characterController.JumpForce -= 10f;
        }
    }   
}
