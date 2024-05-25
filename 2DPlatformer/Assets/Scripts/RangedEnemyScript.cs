using System.Collections;
using UnityEngine;

public class RangedEnemyScript : MonoBehaviour
{
    public RangedEnemy enemy;
    public Weapons enemyWeapon;
    private Transform gunTransform;
    public GameObject spawnPoints;
    private Animator an;
    

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

        an = GetComponent<Animator>();

    }

    
    void Update()
    {
        if (enemy.isDying())
        {
            TriggerDeathAnimation();
            enemyWeapon.spawnGunPrefab(enemyWeapon, transform.position, transform.rotation);
            StartCoroutine(DestroyAfterAnimation());
        }
    }
    void TriggerDeathAnimation()
    {
        if (an != null)
        {
            an.SetTrigger("Death");
           
        }
    }
    IEnumerator DestroyAfterAnimation()
    {
        // Wait for the length of the death animation before destroying the object
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
