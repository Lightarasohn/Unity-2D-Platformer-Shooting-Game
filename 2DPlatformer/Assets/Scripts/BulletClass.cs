using UnityEngine;

public class Bullet
{
    protected float bulletDamage;
    protected float bulletSpeed = 80f;
    private Rigidbody2D bulletRigidbody;
    private float bulletVelocityX;
    private float bulletVelocityY;
    private float bulletRotation;

    public float getBulletDamage()
    {
        return this.bulletDamage;
    }
    public float getBulletSpeed()
    {
        return this.bulletSpeed;
    }
    public Rigidbody2D getBulletRigidbody()
    {
        return this.bulletRigidbody;
    }
    public void setBulletRigidbody(Rigidbody2D bulletRigidbody)
    {
        this.bulletRigidbody = bulletRigidbody;
    }
    public float getBulletVelocityX()
    {
        return this.bulletVelocityX;
    }
    public void setBulletVelocityX(float bulletVelocityX)
    {
        this.bulletVelocityX = bulletVelocityX;
    }
    public float getBulletVelocityY()
    {
        return this.bulletVelocityY;
    }
    public void setBulletVelocityY(float bulletVelocityY)
    {
        this.bulletVelocityY = bulletVelocityY;
    }
    public float getBulletRotation()
    {
        return this.bulletRotation;
    }
    public void setBulletRotation(float bulletRotation)
    {
        this.bulletRotation = bulletRotation;
    }
}
public class Bullet1 : Bullet
{
    public Bullet1()
    {
        base.bulletDamage = 21f;
    }
}
public class Bullet2 : Bullet
{
    public Bullet2()
    {
        base.bulletDamage = 30f;
    }
}
public class Bullet3 : Bullet
{
    public Bullet3()
    {
        base.bulletDamage = 25f;
    }
}
public class Bullet4 : Bullet
{
    public Bullet4()
    {
        base.bulletDamage = 24f;
    }
}
public class Bullet5 : Bullet
{
    public Bullet5()
    {
        base.bulletDamage = 40f;
    }
}