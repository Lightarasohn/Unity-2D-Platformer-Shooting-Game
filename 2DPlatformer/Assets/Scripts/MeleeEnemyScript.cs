using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyScript : MonoBehaviour
{
    public MeleeEnemy enemy;
    private Transform playerTransform;
    private bool isAgroed = false;
    private float timer = 8f;
    private Rigidbody2D enemyRb;
    private bool isStart = true;

    void Start()
    {
        enemyRb = transform.GetComponent<Rigidbody2D>();
        enemy = new MeleeEnemy();
        transform.GetComponent<SpriteRenderer>().sprite = enemy.getBodySprite();
        transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = enemy.getHandSprite();
        transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sprite = enemy.getStaticArmSprite();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isAgroed || enemy.isAgro(transform))
        {
            if (enemy.getAgroDistance() >= enemy.distanceToPlayer(playerTransform))
            {
                
                enemyRb.velocity = new Vector2(enemy.getAgroMovespeed() * (transform.localScale.x * 10 / 7)  , enemyRb.velocity.y);
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
                
            }else if(timer >= enemy.getVoltaTime())
            {
                enemyRb.velocity = new Vector2(0, 0);
            }
            timer += Time.deltaTime;
        }
        isAgroed = enemy.isAgro(transform);
    }
}
