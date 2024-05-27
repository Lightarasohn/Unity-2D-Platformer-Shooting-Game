using System.Collections;
using UnityEngine;

public class RangedEnemyCombatScript : MonoBehaviour
{
    public float angle;
    public bool isAgroed = false;
    private Weapons enemyWeapon;
    private RangedEnemy enemy;
    private Transform parentEnemy;
    private Transform playerTransform;
    private GameObject spawnPoints;
    private bool canShoot = false;
    private float semiTime;
    private bool canAngle = false;
    public bool isDead = false;
    private Animator animator;
    void Start()
    {
        angle = 0;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        parentEnemy = transform.parent.transform.parent.transform;
        enemy = parentEnemy.GetComponent<RangedEnemyScript>().enemy;
        enemyWeapon = parentEnemy.GetComponent<RangedEnemyScript>().enemyWeapon;
        spawnPoints = parentEnemy.GetComponent<RangedEnemyScript>().spawnPoints;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnPoints == null) spawnPoints = parentEnemy.GetComponent<RangedEnemyScript>().spawnPoints;
        if (enemy == null) enemy = parentEnemy.GetComponent<RangedEnemyScript>().enemy;
        if (enemyWeapon == null) enemyWeapon = parentEnemy.GetComponent<RangedEnemyScript>().enemyWeapon;
        if (GameObject.FindGameObjectWithTag("Player") != null && !GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead() && Time.timeScale > 0 && !enemy.isDying())
        {

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
        else
        {
            enemy.setAgroDistance(enemy.getStaticAgroDistance());
        }
        semiTime += Time.deltaTime;
        
        if (semiTime >= enemyWeapon.getFireRate())
        {
            if (canShoot && !enemy.isDying())
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
        if (GameObject.FindGameObjectWithTag("Player") != null && !GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead() && Time.timeScale > 0 && enemy != null && !enemy.isDying())
            if (canAngle) { angle = enemy.rotateGun(transform); }
    }
   
}
