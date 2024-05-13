using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyScript : MonoBehaviour
{
    public RangedEnemy enemy;
    public Weapons enemyWeapon;
    private Transform gunTransform;
    public GameObject spawnPoints;
    
    void Start()
    {
        
        enemy = new RangedEnemy();
        enemyWeapon = enemy.GetWeapon();
        gunTransform = transform.GetChild(0).transform.GetChild(0).transform;
        transform.GetComponent<SpriteRenderer>().sprite = enemy.getBodySprite();
        transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = enemy.getHandSprite();
        transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sprite = enemy.getStaticArmSprite();
        gunTransform.GetComponent<SpriteRenderer>().sprite = enemyWeapon.getSprite();
        spawnPoints = enemyWeapon.instantiateBulletSpawnPoints(gunTransform);
    }

    
    void Update()
    {
        
    }
}
