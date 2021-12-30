using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charecter : Stats
{
    //Stats
    [SerializeField] protected CharecterData charecterData;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpForce;

    protected Rigidbody2D rb;

    
    public void SetCharecter(CharecterData charecter = null)
    {
        charecterData = charecter;

        if(charecterData == null)
        {
            Debug.Log("No Charecter Data Found " + gameObject.name);
            Destroy(gameObject);
            return;
        } else
        InitializeStats();
    }

    protected override void InitializeStats()
    {
        rb = GetComponent<Rigidbody2D>();

        healthMax = charecterData.healthMax;
        shieldMax = charecterData.shieldMax;
        moveSpeed = charecterData.moveSpeed;
        jumpForce = charecterData.jumpForce;
        rb.gravityScale = charecterData.gravityScale;

        base.InitializeStats();
    }

    public override void ModifyStatAdditive(StatModifiers stat, float value)
    {
        base.ModifyStatAdditive(stat, value);

        if(stat == StatModifiers.Speed)
        {
            moveSpeed = charecterData.moveSpeed + modifiers[stat];

            if (moveSpeed <= 0)
            {
                moveSpeed = charecterData.moveSpeed * 0.1f;
            }
        }

        if(stat == StatModifiers.JumpForce)
        {
            jumpForce = charecterData.jumpForce + modifiers[stat];

            if (jumpForce <= 0)
            {
                jumpForce = charecterData.jumpForce * 0.1f;
            }
        }

        if(stat == StatModifiers.Gravity)
        {
            rb.gravityScale = charecterData.gravityScale + modifiers[stat];

            if (rb.gravityScale > -1 && rb.gravityScale <= 1)
            {
                rb.gravityScale = 1;
            }
        }
    }

    public override void ModifyStatMultiplicative(StatModifiers stat, float value)
    {
        base.ModifyStatMultiplicative(stat, value);

        if(stat == StatModifiers.Speed)
        {
            modifiers[stat] += (moveSpeed * value);

            moveSpeed = charecterData.moveSpeed + modifiers[stat];

            if (moveSpeed <= 0)
            {
                moveSpeed = charecterData.moveSpeed * 0.1f;
            }
        }

        if(stat == StatModifiers.JumpForce)
        {
            modifiers[stat] += (jumpForce * value);

            jumpForce = charecterData.jumpForce + modifiers[stat];

            if (jumpForce <= 0)
            {
                jumpForce = charecterData.jumpForce * 0.1f;
            }
        }

        if(stat == StatModifiers.Gravity)
        {
            modifiers[stat] += (rb.gravityScale * value);
            rb.gravityScale = charecterData.gravityScale + modifiers[stat];

            if (rb.gravityScale > -1 && rb.gravityScale <= 1)
            {
                rb.gravityScale = 1;
            }
        }
    }

    public override void ResetStats()
    {
        base.ResetStats();

        healthMax = charecterData.healthMax;
        shieldMax = charecterData.shieldMax;
        moveSpeed = charecterData.moveSpeed;
        jumpForce = charecterData.jumpForce;
        rb.gravityScale = charecterData.gravityScale;

        if(healthCurrent > healthMax){
            healthCurrent = healthMax;
        }

        if(shieldCurrent > shieldMax){
            shieldCurrent = shieldMax;
        }
    }
}
