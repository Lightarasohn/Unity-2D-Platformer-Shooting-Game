using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public MeleeEnemy.BossEnemy enemy;
    public bool isAgroed = false;
    private Transform playerTransform;
    private float timer = 8f;
    private Rigidbody2D enemyRb;
    private bool isStart = true;
    private BoxCollider2D boxCollider;
    private float hitTimer;
    private float inCreaseSpeedTimer;
    private float deCreaseSpeedTimer;
    private System.Random indexGenerator;

    void Start()
    {
        enemyRb = transform.GetComponent<Rigidbody2D>();
        enemy = new MeleeEnemy.BossEnemy();
        transform.GetComponent<SpriteRenderer>().sprite = enemy.getBodySprite();
        transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = enemy.getHandSprite();
        transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sprite = enemy.getStaticArmSprite();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider = transform.GetComponent<BoxCollider2D>();
        
        enemy.setAudioSource(transform);
        enemy.setAudioBossMusic(transform);

        indexGenerator = new System.Random();
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && !GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead() && Time.timeScale == 1)
        {
            if (enemy.isDying())
            {
                enemy.TriggerDeathAnimation();
                StartCoroutine(enemy.DestroyAfterAnimation(gameObject));
            }
            
            enemy.setAnimation(transform);
            enemy.setWalkingSound(indexGenerator.Next(1, 3));
            enemy.setHitSound(indexGenerator.Next(1, 3));
            enemy.playBossMusic();

            if (isAgroed || enemy.isAgro(transform))
            {
                if (enemy.getAgroDistance() >= enemy.distanceToPlayer(playerTransform))
                {
                    inCreaseSpeedTimer += Time.deltaTime;
                    if(inCreaseSpeedTimer >= 4)
                    {
                        enemyRb.velocity = new Vector2((2f) * enemy.getAgroMovespeed() * (transform.localScale.x * 10 / 7), enemyRb.velocity.y);
                        deCreaseSpeedTimer += Time.deltaTime;
                        enemy.getAnimator().speed = 1f;
                        if (deCreaseSpeedTimer >= 4)
                        {
                            inCreaseSpeedTimer = 0;
                        }
                    }
                    else
                    {
                        deCreaseSpeedTimer = 0;
                        enemyRb.velocity = new Vector2(enemy.getAgroMovespeed() * (transform.localScale.x * 10 / 7), enemyRb.velocity.y);
                    }

                    if (enemy.canHit(boxCollider, transform))
                    {
                        hitTimer += Time.deltaTime;
                        if (hitTimer >= 2)
                        {
                            hitTimer = 0;
                            enemy.PlayHitSound();
                            GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().hurtPlayer(100f);
                        }
                    }

                    if (enemy.isPlayerBehind(transform))
                    {

                        enemy.flipEnemy(transform);
                    }
                }
                else
                {

                    timer = enemy.getVoltaTime();
                }
            }
            else
            {
                enemy.setAgroDistance(enemy.getStaticAgroDistance());
                if (timer >= enemy.getVoltaTime() + 1.5f)
                {
                    if (!isStart) enemy.flipEnemy(transform);
                    else { isStart = false; }
                    enemy.voltaAt(transform, enemyRb);
                    enemy.getAnimator().speed = 1f;
                    timer = 0f;

                }
                else if (timer >= enemy.getVoltaTime())
                {
                    enemyRb.velocity = new Vector2(0, 0);
                    enemy.getAnimator().speed = 0;
                }
                timer += Time.deltaTime;
            }
            isAgroed = enemy.isAgro(transform);
            if (enemyRb.velocity.x != 0)
            {
                if (enemy.canHit(boxCollider, transform))
                {
                    if (hitTimer < 2)
                    {
                        enemy.PlayWalkingSound();
                    }
                }
                else
                {
                    enemy.PlayWalkingSound();
                }

            }
        }
    }
}
