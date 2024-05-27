using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class EnemyClass
{
    protected float agroDistance;
    protected float staticAgroDistance;
    protected float health;
    protected Weapons weapon = null;
    protected Sprite bodySprite;
    protected Sprite staticArmSprite;
    protected Sprite handSprite;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected bool isDead = false;


    
    public float getHealth()
    {
        return this.health;
    }
    
    public void setAnimation(Transform transform)
    {
        animator = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
    }
    public Animator getAnimator()
    {
        return this.animator;
    }
    public void TriggerDeathAnimation()
    {
        if (this.animator != null)
        {
            this.animator.SetTrigger("Death");

        }
    }
    public IEnumerator DestroyAfterAnimation(GameObject gameObject)
    {
        // Wait for the length of the death animation before destroying the object
        yield return new WaitForSeconds(0.5f);
        GameObject.Destroy(gameObject);
    }
    public float getStaticAgroDistance()
    {
        return this.staticAgroDistance;
    }
    public float getAgroDistance()
    {
        return this.agroDistance;
    }
    public void setAgroDistance(float distance)
    {
        this.agroDistance = distance;
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
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(transform.position.x - playerTransform.position.x), 2) + Mathf.Pow(Mathf.Abs(transform.position.y - playerTransform.position.y), 2));
    }
    public bool isPlayerBehind(Transform transform)
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (this.isFacingRight(transform) && (playerTransform.position.x <= transform.position.x))
        {
            return true;
        }
        else if (!this.isFacingRight(transform) && (playerTransform.position.x >= transform.position.x))
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
        base.health = 100;
        base.agroDistance = 10;
        base.staticAgroDistance = this.agroDistance;
        base.weapon = pickRangedEnemyGun();
        base.bodySprite = Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedBodies/cyborgidle_0");
        base.staticArmSprite = Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/StaticArms/cyborg static arm");
        base.handSprite = Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/Hands/cyborg hand");
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
        switch (rnd.Next(0, 6))
        {
            case 1:
                wp = new Weapon1(true);
                break;
            case 2:
                wp = new Weapon2(true);
                break;
            case 3:
                wp = new Weapon3(true);
                break;
            case 4:
                wp = new Weapon4(true);
                break;
            case 5:
                wp = new Weapon5(true);
                break;
            default:
                wp = new Weapon1(true);
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
    private AudioSource audioSource;
    private AudioClip walkingSound;
    private AudioClip hitSound;
    
    public MeleeEnemy()
    {
        this.colliderDistance = 0.7f;
        this.hitRange = 2.5f;
        this.voltaTime = 4f;
        this.voltaMovespeed = 1.5f;
        this.agroMovespeed = 7;
        base.health = 100;
        base.agroDistance = 5;
        base.staticAgroDistance = this.agroDistance;
        base.bodySprite = Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedBodies/biker idle_0");
        base.staticArmSprite = Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/StaticArms/biker arm");
        base.handSprite = Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/Hands/biker hands");


    }
    public void setAudioSource(Transform transform)
    {
        this.audioSource = transform.GetComponents<AudioSource>()[0];
    }
    public void setWalkingSound(int index)
    {
        this.walkingSound = Resources.Load<AudioClip>("SoundEffects/EnemySoundEffects/EnemyWalking/EnemyWalking" + index);
    }
    public void setHitSound(int index)
    {
        this.hitSound = Resources.Load<AudioClip>("SoundEffects/EnemySoundEffects/Punch/Punch" + index);
    }
    public void PlayHitSound()
    {
        if (this.audioSource != null && this.hitSound != null)
        {
            this.audioSource.clip = this.hitSound;
            this.audioSource.Play();
        }
    }
    public void PlayWalkingSound()
    {
        if (this.audioSource != null && this.walkingSound != null && !this.audioSource.isPlaying)
        {
            this.audioSource.clip = this.walkingSound;
            this.audioSource.Play();
        }
    }
    public float getAgroMovespeed()
    {
        return this.agroMovespeed;
    }
    public float getVoltaTime()
    {
        return this.voltaTime;
    }
    /*
    public float getHitRange()
    {
        return this.hitRange;
    }
    public float getColliderDistance()
    {
        return this.colliderDistance;
    }
    */
    public void voltaAt(Transform transform, Rigidbody2D rb)
    {
        rb.velocity = new Vector2(this.voltaMovespeed * (transform.localScale.x * 10 / 7), rb.velocity.y);

    }
    public bool canHit(BoxCollider2D boxCollider, Transform transform)
    {
        bool tmp = false;

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * this.hitRange * transform.localScale.x * this.colliderDistance,
            new Vector3(boxCollider.bounds.size.x * this.hitRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0,
            1 << LayerMask.NameToLayer("Action"));
        if (hit.collider != null)
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
    public class BossEnemy : MeleeEnemy
    {
        private AudioSource audioSourceBossMusic;
        private AudioClip bossMusic;
        public BossEnemy()
        {
            this.bossMusic = Resources.Load<AudioClip>("SoundEffects/GameAndMenuSounds/BossMusic");
            base.colliderDistance = 0.5f;
            base.hitRange = 2.5f;
            base.voltaTime = 4f;
            base.voltaMovespeed = 1.5f;
            base.agroMovespeed = 5;
            base.health = 500;
            base.agroDistance = 15;
            base.staticAgroDistance = this.agroDistance;
            base.bodySprite = Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedBodies/biker idle_0");
            base.staticArmSprite = Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/StaticArms/biker arm");
            base.handSprite = Resources.Load<Sprite>("Sprites/EnemySprites/SeperatedArms/Hands/biker hands");
        }
        public void setAudioBossMusic(Transform transform)
        {
            this.audioSourceBossMusic = transform.GetComponents<AudioSource>()[1];
        }
        public void playBossMusic()
        {
            this.audioSourceBossMusic.clip = bossMusic;
            if (!this.audioSourceBossMusic.isPlaying)
            {
                audioSourceBossMusic.Play();
            }
        }
    }
}
