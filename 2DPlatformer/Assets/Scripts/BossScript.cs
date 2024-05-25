using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public BossEnemy enemy;
    private Transform playerTransform;
    private bool isAgroed = false;
    private float timer = 8f;
    private Rigidbody2D enemyRb;
    private bool isStart = true;
    private BoxCollider2D boxCollider;
    private float hitTimer;
    private float inCreaseSpeedTimer;
    private float deCreaseSpeedTimer;

    void Start()
    {
        enemyRb = transform.GetComponent<Rigidbody2D>();
        enemy = new BossEnemy();
        transform.GetComponent<SpriteRenderer>().sprite = enemy.getBodySprite();
        transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = enemy.getHandSprite();
        transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sprite = enemy.getStaticArmSprite();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider = transform.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && !GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead() && Time.timeScale == 1)
        {
            if (enemy.isDying())
            {
                GameObject.Destroy(gameObject);
            }

            if (isAgroed || enemy.isAgro(transform))
            {
                if (enemy.getAgroDistance() >= enemy.distanceToPlayer(playerTransform))
                {
                    inCreaseSpeedTimer += Time.deltaTime;
                    if(inCreaseSpeedTimer >= 4)
                    {
                        enemyRb.velocity = new Vector2((2f) * enemy.getAgroMovespeed() * (transform.localScale.x * 10 / 7), enemyRb.velocity.y);
                        deCreaseSpeedTimer += Time.deltaTime;
                        if(deCreaseSpeedTimer >= 4)
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
                            Debug.Log("Hit !!!");
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
                if (timer >= enemy.getVoltaTime() + 1.5f)
                {
                    if (!isStart) enemy.flipEnemy(transform);
                    else { isStart = false; }
                    enemy.voltaAt(transform, enemyRb);
                    timer = 0f;

                }
                else if (timer >= enemy.getVoltaTime())
                {
                    enemyRb.velocity = new Vector2(0, 0);
                }
                timer += Time.deltaTime;
            }
            isAgroed = enemy.isAgro(transform);
        }
    }
}
