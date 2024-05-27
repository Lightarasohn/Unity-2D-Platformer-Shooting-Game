using UnityEngine;

public class BulletCrashLogicEnemy : MonoBehaviour
{
    private float Damage;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<BulletPrefabMovementEnemy>().bullet != null) Damage = transform.GetComponent<BulletPrefabMovementEnemy>().bullet.getBulletDamage();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Rigidbody2D>() != null)
            collision.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (collision.transform.tag == "Player")
        {
            if (!collision.transform.GetComponent<PlayerMovement>().isDashing)
            {
                GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().hurtPlayer(Damage);
            }
            Destroy(gameObject);
        }
        else if (collision.transform.tag != "Bullet(Enemy)")
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Rigidbody2D>() != null)
            collision.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (collision.transform.tag != "Bullet(Enemy)")
        {
            Destroy(gameObject);
        }
    }
}
