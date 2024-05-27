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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Rigidbody2D>() != null)
            collision.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (collision.transform.tag == "RangedEnemy")
        {
            rEnemy = collision.transform.GetComponent<RangedEnemyScript>().enemy;
            rEnemy.hurt(Damage);
            rEnemy.setAgroDistance(rEnemy.distanceToPlayer(collision.transform));
            collision.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<RangedEnemyCombatScript>().isAgroed = true;
            Destroy(gameObject);
        }
        else if (collision.transform.tag == "MeleeEnemy")
        {
            mEnemy = collision.transform.GetComponent<MeleeEnemyScript>().enemy;
            mEnemy.hurt(Damage);
            mEnemy.setAgroDistance(mEnemy.distanceToPlayer(collision.transform));
            collision.transform.GetComponent<MeleeEnemyScript>().isAgroed = true;
            Destroy(gameObject);
        }
        else if (collision.transform.tag == "BossEnemy")
        {
            bossEnemy = collision.transform.GetComponent<BossScript>().enemy;
            bossEnemy.hurt(Damage);
            bossEnemy.setAgroDistance(bossEnemy.distanceToPlayer(collision.transform));
            collision.transform.GetComponent<BossScript>().isAgroed = true;
            Destroy(gameObject);
        }
        else if (collision.transform.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Rigidbody2D>() != null)
            collision.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (collision.transform.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
