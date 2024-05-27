using UnityEngine;

public class BulletPrefabMovementEnemy : MonoBehaviour
{
    public Bullet bullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bullet == null)
        {
            bullet = pickBullet();
            bullet.setBulletRigidbody(transform.GetComponent<Rigidbody2D>());
            bullet.setBulletRotation(transform.rotation.eulerAngles.z);
            bullet.setBulletVelocityX(Mathf.Cos(bullet.getBulletRotation() * Mathf.Deg2Rad) * bullet.getBulletSpeed());
            bullet.setBulletVelocityY(Mathf.Sin(bullet.getBulletRotation() * Mathf.Deg2Rad) * bullet.getBulletSpeed());
        }
        if (bullet != null) bullet.getBulletRigidbody().velocity = new Vector2(bullet.getBulletVelocityX(), bullet.getBulletVelocityY());
    }

    private Bullet pickBullet()
    {
        switch (transform.name)
        {
            case "Weapon1Bullet(Enemy)(Clone)":
                return new Bullet1();
            case "Weapon2Bullet(Enemy)(Clone)":
                return new Bullet2();
            case "Weapon3Bullet(Enemy)(Clone)":
                return new Bullet3();
            case "Weapon4Bullet(Enemy)(Clone)":
                return new Bullet4();
            case "Weapon5Bullet(Enemy)(Clone)":
                return new Bullet5();
            default:
                return null;
        }
    }
}
