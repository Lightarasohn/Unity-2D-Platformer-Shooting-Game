using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class EnemyClass
{
    private float agroDistance;
    private float health;
    private Weapons weapon = null;
    private Sprite bodySprite;
    private Sprite staticArmSprite;
    private Sprite handSprite;

    public float getHealth()
    {
        return this.health;
    }
    public float getAgroDistance()
    {
        return this.agroDistance;
    }
    protected void setAgroDistance(float agro)
    {
        this.agroDistance = agro;
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
    protected void setHealth(float health)
    {
        this.health = health;
    }
    public void hurt(float damage)
    {
        this.health -= damage;
    }
    public bool isDying()
    {
        return this.health <= 0;
    }
    public float distanceToPlayer(Transform transform)
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(transform.position.x - playerTransform.position.x),2) + Mathf.Pow(Mathf.Abs(transform.position.y - playerTransform.position.y),2));
    }
    public bool isPlayerBehind(Transform transform)
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (this.isFacingRight(transform) && (playerTransform.position.x <= transform.position.x))
        {
            return true;
        }
        else if(!this.isFacingRight(transform) && (playerTransform.position.x >= transform.position.x))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isFacingRight(Transform transform)
    {
        return transform.localScale.x > 0;
    }
    public bool isAgro(Transform transform)
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (this.isFacingRight(transform) && !this.isPlayerBehind(transform) && this.distanceToPlayer(transform) <= this.getAgroDistance())
        {
            return true;
        }
        else if (!this.isFacingRight(transform) && !this.isPlayerBehind(transform) && this.distanceToPlayer(transform) <= this.getAgroDistance())
        {
            return true;
        }
        else
            return false;
    }
    public void flipEnemy(Transform transform)
    {
        Vector3 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;
    }

}

public class RangedEnemy : EnemyClass
{
    
    public RangedEnemy()
    {
        base.setHealth(100);
        base.setAgroDistance(10);
        base.setWeapon(pickRangedEnemyGun());
        base.setBodySprite(Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedBodies/cyborgidle_0"));
        base.setStaticArmSprite(Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/StaticArms/cyborg static arm"));
        base.setHandSprite(Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/Hands/cyborg hand"));
    
    }
    public float rotateGun(Transform transform) 
    {
        float angle;
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 playerPos = playerTransform.position;
        Vector2 vector = (playerPos - (Vector2)(transform.parent.position));
        angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.parent.rotation = rotation;

        this.flipGunAndArms(transform);
        return angle;
    }
    private void flipGunAndArms(Transform transform)
    {
        SpriteRenderer sr = transform.GetComponent<SpriteRenderer>();
        SpriteRenderer srParent = transform.parent.transform.GetComponent<SpriteRenderer>();
        if (!this.isFacingRight(transform.parent.transform.parent.transform))
        {
            if (!sr.flipX)
            {
                
                srParent.flipX = true;
                srParent.flipY = true;
                sr.flipX = true;
                sr.flipY = true;
            }
        }
        else
        {
            srParent.flipX = false;
            srParent.flipY = false;
            sr.flipY = false;
            sr.flipX = false;
        }
    }
    private Weapons pickRangedEnemyGun()
    {
        Weapons wp;
        Random rnd = new Random();
        switch (rnd.Next(0,5)) 
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
    private float voltaMovespeed;
    private float agroMovespeed;
    private float voltaTime;
    private float hitRange;
    private float colliderDistance;
    public MeleeEnemy()
    {
        this.colliderDistance = 0.7f;
        this.hitRange = 2.5f;
        this.voltaTime = 4f;
        this.voltaMovespeed = 1.5f;
        this.agroMovespeed = 3;
        base.setHealth(100);
        base.setAgroDistance(10);
        base.setBodySprite(Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedBodies/biker idle_0"));
        base.setStaticArmSprite(Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/StaticArms/biker arm"));
        base.setHandSprite(Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/Hands/biker hands"));
    }
    public float getAgroMovespeed()
    {
        return this.agroMovespeed;
    }
    public float getVoltaTime()
    {
        return this.voltaTime;
    }
    public float getHitRange()
    {
        return this.hitRange;
    }
    public float getColliderDistance()
    {
        return this.colliderDistance;
    }
    public void voltaAt(Transform transform, Rigidbody2D rb)
    {
        rb.velocity = new Vector2(this.voltaMovespeed * (transform.localScale.x * 10 / 7), rb.velocity.y);
        
    }

    public bool canHit(BoxCollider2D boxCollider, Transform transform)
    {
        bool tmp = false;

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * getHitRange() * transform.localScale.x * getColliderDistance(),
            new Vector3(boxCollider.bounds.size.x * getHitRange(), boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0,
            1 << LayerMask.NameToLayer("Action"));

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                tmp = true;
            }
            else
            {
                tmp = false;
            }
        }
        return tmp;
    }
   
}
