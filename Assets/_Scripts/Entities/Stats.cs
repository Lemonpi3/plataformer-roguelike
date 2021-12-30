using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    // This is the base stat class where all enteties that can recibe damage derive from.

    [SerializeField] protected int healthMax = 1;

    protected int healthMaxDefault {get; set;}

    [SerializeField] protected int _healthCurrent; 

    [SerializeField] protected int healthCurrent {

        get => _healthCurrent;
        set
        {
            if (value > healthMax)
            {
                healthCurrent = healthMax;
            }
            else if (value <= 0)
            {
                Die();
            }
            else
            {
                _healthCurrent = value;
            }
        }
    }

    [SerializeField] protected int shieldMax = 0;
    protected int shieldMaxDefault {get;set;}

    [SerializeField] protected int _shieldCurrent = 0;

    [SerializeField] protected int shieldCurrent{

        get => _shieldCurrent;
        set
        {
            if(value > shieldMax)
            {
                shieldCurrent = shieldMax;
            } 
            else if(value <=0)
            {
                _shieldCurrent = 0;
            }
            else
            {
                _shieldCurrent = value;
            }
        }
    }

    [SerializeField] protected Dictionary<StatModifiers,float> modifiers = new Dictionary<StatModifiers,float>();
    
    ///<summary>By Default: Initialize stats</summary>
    protected virtual void Start() 
    {
        InitializeStats();
    }
    
    protected virtual void InitializeStats()
    {
        healthCurrent = healthMax;
        shieldCurrent = shieldMax;
        healthMaxDefault = healthMax;
        shieldMaxDefault = shieldMax;

        modifiers.Add(StatModifiers.Health,0);
        modifiers.Add(StatModifiers.Shield,0);
        modifiers.Add(StatModifiers.Speed,0);
        modifiers.Add(StatModifiers.JumpForce,0);
        modifiers.Add(StatModifiers.Gravity,0);
    }

    public virtual void ModifyStatAdditive(StatModifiers stat,float value)
    {
        modifiers[stat] += value;

        if(stat == StatModifiers.Health)
        {
            healthMax = healthMaxDefault + (int)modifiers[stat]; 

            if(healthMax <= 0){
                healthMax = 1;
                healthCurrent = healthMax; //to prevent negative health
            }
        }
        if(stat == StatModifiers.Health)
        {
            shieldMax = shieldMaxDefault + (int)modifiers[stat]; 

            if(shieldMax <= 0){
                shieldMax = 0;
                shieldCurrent = shieldMax; //to prevent negative shield
            }
        }
    }

    ///<summary>Adds a modifier based on a multiplicative value of the current stat 
    ///ex: 
    ///    maxHealth = 100
    ///    ModifyStatMultiplicative(Health,0.5)
    /// 
    /// result:
    ///    modifiers[Health] += (maxHealth * 0.5)
    ///    maxHealth = maxHealthDefault + modifiers[Health]
    ///
    ///    0 += (100* 0.5)
    ///    maxHealth = 150
    ///     
    ///</summary>
    public virtual void ModifyStatMultiplicative(StatModifiers stat,float value)
    {
        if(stat == StatModifiers.Health)
        {
            modifiers[stat] += (int)(healthMax * value);
            healthMax = healthMaxDefault + (int)modifiers[stat]; 

            if(healthMax <= 0){
                healthMax = 1;
                healthCurrent = healthMaxDefault; //to prevent negative health
            }
        }

        if(stat == StatModifiers.Health)
        {
            modifiers[stat] += (int)(shieldMax * value);
            shieldMax = shieldMaxDefault + (int)modifiers[stat]; 

            if(shieldMax <= 0){
                shieldMax = 0;
                shieldCurrent = shieldMaxDefault; //to prevent negative shield
            }
        }
    }

    public virtual void ResetStats()
    {
        healthMax = healthMaxDefault;
        shieldMax = shieldMaxDefault;

        foreach(KeyValuePair<StatModifiers,float> modifier in modifiers)
        {
            modifiers[modifier.Key] = 0;
        }
    }

    public virtual void TakeDamage(int amount)
    {
        int remanent = _shieldCurrent - amount;
        if(remanent < 0)
        {
            _healthCurrent += remanent; // remanent will be a negative number if this condition happens.
        }
    }

    ///<summary>Destroys gameObject</summary>
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}

public enum StatModifiers {Health,Shield,Speed,JumpForce,Gravity}