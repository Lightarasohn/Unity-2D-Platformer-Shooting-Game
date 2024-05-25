using UnityEngine;

public class BulletCrashLogic : MonoBehaviour
{
    private RangedEnemy rEnemy;
    private MeleeEnemy mEnemy;
    private MeleeEnemy.BossEnemy bossEnemy;
    private float Damage;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.GetComponent<BulletPrefabMovement>().bullet != null) Damage = transform.GetComponent<BulletPrefabMovement>().bullet.getBulletDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "RangedEnemy")
        {
            rEnemy = collision.transform.GetComponent<RangedEnemyScript>().enemy;
            rEnemy.hurt(Damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "MeleeEnemy")
        {
            mEnemy = collision.transform.GetComponent<MeleeEnemyScript>().enemy;
            mEnemy.hurt(Damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "BossEnemy")
        {
            bossEnemy = collision.transform.GetComponent<BossScript>().enemy;
            bossEnemy.hurt(Damage);
            Destroy (gameObject);
        }
        else if(collision.tag == "Player")
        {
            collision.transform.GetComponent<PlayerHealth>().hurtPlayer(Damage);
            Destroy(gameObject);
        }
        else if (collision.tag != "Bullet")
        {
             Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "RangedEnemy")
        {
            rEnemy = collision.transform.GetComponent<RangedEnemyScript>().enemy;
            rEnemy.hurt(Damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "MeleeEnemy")
        {
            mEnemy = collision.transform.GetComponent<MeleeEnemyScript>().enemy;
            mEnemy.hurt(Damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
            collision.transform.GetComponent<PlayerHealth>().hurtPlayer(Damage);
            Destroy(gameObject);
        }
        else if (collision.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
