using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class EnemyClass
{
    private float health;
    private Weapons weapon = null;
    private Sprite bodySprite;
    private Sprite staticArmSprite;
    private Sprite handSprite;

    public float getHealth()
    {
        return this.health;
    }
    public Weapons GetWeapon()
    {
        return this.weapon;
    }
    public Sprite getBodySprite()
    {
        return this.bodySprite;
    }
    public Sprite getStaticArmSprite() 
    {
        return this.staticArmSprite;
    }
    public Sprite getHandSprite()
    {
        return this.handSprite;
    }
    protected void setBodySprite(Sprite bd)
    {
        this.bodySprite = bd;
    }
    protected void setWeapon(Weapons weapon)
    {
        this.weapon = weapon;
    }
    protected void setHandSprite(Sprite handSprite)
    {
        this.handSprite = handSprite;
    }
    protected void setStaticArmSprite(Sprite staticArmSprite)
    {
        this.staticArmSprite = staticArmSprite;
    }
    public void setHealth(float health)
    {
        this.health = health;
    }

    public void isDying(GameObject GO)
    {
        if(health <= 0)
        {
            GameObject.Destroy(GO);
        }
    }
}

public class RangedEnemy : EnemyClass
{
    
    public RangedEnemy()
    {
        base.setWeapon(pickRangedEnemyGun());
        base.setBodySprite(Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedBodies/Idle_0"));
        base.setStaticArmSprite(Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/StaticArms/MainArms1"));
        base.setHandSprite(Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/Hands/MainArms2.png"));
    
    }
    
    private Weapons pickRangedEnemyGun()
    {
        Weapons wp;
        Random rnd = new Random();
        switch (rnd.Next(1,5)) 
        {
            case 1:
                wp = new Weapon1();
                break;
            case 2:
                wp = new Weapon2();
                break;
            case 3:
                wp = new Weapon3();
                break;
            case 4:
                wp = new Weapon4();
                break;
            case 5:
                wp = new Weapon5();
                break;
            default:
                wp = new Weapon1();
                break;
        }
        return wp;
    }

    
}

public class MeleeEnemy : EnemyClass
{
    public MeleeEnemy()
    {
        
    }
}
