using UnityEngine;

public class RangedEnemyCombatScript : MonoBehaviour
{
    public float angle;
    private Weapons enemyWeapon;
    private RangedEnemy enemy;
    private Transform parentEnemy;
    private Transform playerTransform;
    private bool isAgroed = false;
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
        if (GameObject.FindGameObjectWithTag("Player") != null && !GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead() && Time.timeScale > 0 && !enemy.isDying())
            if (canAngle) { angle = enemy.rotateGun(transform); }
    }
}
