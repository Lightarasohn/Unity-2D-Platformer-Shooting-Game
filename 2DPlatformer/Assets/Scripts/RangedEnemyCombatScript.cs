using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyCombatScript : MonoBehaviour
{
    private Weapons enemyWeapon;
    private RangedEnemy enemy;
    private Transform parentEnemy;
    private Transform playerTransform;
    private bool isAgroed = false;
    public float angle;
    private GameObject spawnPoints;
    private bool canShoot = false;
    private float semiTime;
    private bool canAngle = false;
    void Start()
    {
        angle = 0;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        parentEnemy = transform.parent.transform.parent.transform;
        enemy = parentEnemy.GetComponent<RangedEnemyScript>().enemy;
        enemyWeapon = parentEnemy.GetComponent<RangedEnemyScript>().enemyWeapon;
        spawnPoints = parentEnemy.GetComponent<RangedEnemyScript>().spawnPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && !GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead() && Time.timeScale == 1)
        {
            if(spawnPoints == null) spawnPoints = parentEnemy.GetComponent<RangedEnemyScript>().spawnPoints;
        if (enemy == null) enemy = parentEnemy.GetComponent<RangedEnemyScript>().enemy;
        if(enemyWeapon == null) enemyWeapon = parentEnemy.GetComponent<RangedEnemyScript>().enemyWeapon;
        if (isAgroed || enemy.isAgro(parentEnemy))
        {    
            if (enemy.getAgroDistance() >= enemy.distanceToPlayer(playerTransform))
            {
                canAngle = true;
                canShoot = true;
                if (enemy.isPlayerBehind(parentEnemy))
                {
                    enemy.flipEnemy(parentEnemy);
                }
            }
        }
        semiTime += Time.deltaTime;
        
        if (semiTime >= enemyWeapon.getFireRate())
        {
            if (canShoot)
            {
                enemyWeapon.fire(spawnPoints, transform, enemyWeapon);
                semiTime = 0;
            }
            
        }
        canShoot = false;
            isAgroed = enemy.isAgro(parentEnemy);
        }
    }

    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && !GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead() && Time.timeScale == 1)
            if (canAngle) { angle = enemy.rotateGun(transform); }
    }
}
