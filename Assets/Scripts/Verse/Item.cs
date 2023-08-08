using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ThingWithComp
{
    public bool melee;
    public bool magic;
    public bool ranged;
    public bool summon;

    public int useTime; //Time
    public int useReloadTime;

    public int stack;
    public int maxStack;

    public int damage;
    public float critChance;
    public float knockBack;
    
    #region potionState
    public int healLife;
    public int healMana;
    public int buffTime;
    public bool potion;
    #endregion

    public int defense;
    
    public int health;
    public int healthRegen;//hoi mau theo giay
    public int healthIncrease;

    public int manaIncrease;
    public int manaRegen;
    public int mana;

    public int manaCost;

    /// <summary>
	/// Check can you hold to auto use
	/// </summary>
    public bool autoReuse;

    
    public float Payload; // kg
    public float GetAllPayLoad
    {
        get
        {
            return Payload * stack;
        }
    }
    public virtual void TickOnMap(){}
    public virtual void UseEffects(){}
    public virtual bool CanUseItem(Player player)
	{
		return true;
	}
    public virtual void OnUse(){}

    public override void Tick()
    {
        base.Tick();
        TickOnMap();
        DrawGizmoHitBox();
        OnUse();
        UseEffects();
    }
    
    public virtual void DrawGizmoHitBox()
    {
        
    }
    
}
