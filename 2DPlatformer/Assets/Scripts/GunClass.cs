using System.Diagnostics.Tracing;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;



public class Weapons : Behaviour
{
    private string gunName;
    private int ammo;
    private int staticAmmo;
    private int mags;
    private Sprite sprite;
    private float fireRate;
    private float damage;
    private string bulletPoints;
    private string weaponBullet;

    

    public void spawnGunPrefab(Weapons gunType, Vector2 gunPosition, Quaternion gunRotation)
    {
        GameObject gun;
        gun = Resources.Load<GameObject>("Prefabs/GunSpawnPrefab");

        gun.transform.GetComponent<GunSpawnPrefabScript>().droppedWeapon = gunType;
        Instantiate(gun, gunPosition, gunRotation);
        
    }

    public void reload()
    {
        if(mags > 0)
        {
            mags--;
            ammo = staticAmmo;
        }
        
    }

    public GameObject instantiateBulletSpawnPoints(Transform transform)
    {
        GameObject spawnpoints;
        spawnpoints = Resources.Load<GameObject>("WeaponsBulletSpawnPoints/" + this.getBulletPoints());
        
        Instantiate(spawnpoints, transform);

        return spawnpoints;
    }

    public void eraseOldBulletSpawnPoints()
    {
        var oldSpawnPoints = GameObject.FindGameObjectWithTag("BulletSpawnPointTag");
        GameObject.Destroy(oldSpawnPoints);
    }

    public void fire(GameObject spawnpoints, Transform transform)
    {
        Weapons currentWeapon;
        GameObject bullet = Resources.Load<GameObject>("Prefabs/" + this.getWeaponBullet());
        PlayerMovement pm;
        EnemyClass enemy;
        if (transform.tag == "PlayerGunPoint")
        {
            currentWeapon = transform.GetComponent<GunPick>().currentWeapon;
            pm = transform.parent.parent.GetComponent<PlayerMovement>();
            enemy = null;
        }
        else //else if olacak ve dusmana gore doldurulacak
        {
            currentWeapon = transform.parent.transform.parent.transform.GetComponent<RangedEnemyScript>().enemyWeapon;
            enemy = transform.parent.transform.parent.transform.GetComponent<RangedEnemyScript>().enemy;
            pm = null;
        }
        Transform front = spawnpoints.transform.GetChild(0);
        Transform back = spawnpoints.transform.GetChild(1);
        if (pm != null)
        {
            if (currentWeapon is Rifles)
            {
                if (Input.GetMouseButton(0))
                {
                    if (pm.isFacingRight)
                    {
                        var insPosition = new Vector3((transform.parent.position.x) + (transform.parent.localScale.x * transform.localPosition.x) + (transform.parent.localScale.x * front.localPosition.x * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * front.localPosition.x * transform.parent.localScale.y, 0);
                        Instantiate(bullet, insPosition, transform.rotation);
                    }
                    else
                    {
                        if (transform.rotation.eulerAngles.z <= 0)
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                            Instantiate(bullet, insPosition, transform.rotation);
                        }
                        else
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                            Instantiate(bullet, insPosition, transform.rotation);
                        }
                    }
                }
            }
            else if (currentWeapon is Shotguns)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    float tempAngle;
                    Quaternion rotation;

                    
                    GunAngle gunAngleScript = transform.GetComponent<GunAngle>();
                    tempAngle = gunAngleScript.angle;
                    

                    if (pm.isFacingRight)
                    {
                        Debug.Log("Shotgun SAG");
                        var insPosition = new Vector3((transform.parent.position.x) + (transform.parent.localScale.x * transform.localPosition.x) + (transform.parent.localScale.x * front.localPosition.x * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * front.localPosition.x * transform.parent.localScale.y, 0);


                        rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                        Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                        Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                        Instantiate(bullet, insPosition, rotation);


                    }
                    else
                    {
                        Debug.Log("Shotgun SOL");
                        if (transform.rotation.eulerAngles.z <= 0)
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);

                            rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                            Instantiate(bullet, insPosition, rotation);
                            rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                            Instantiate(bullet, insPosition, rotation);
                            rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                            Instantiate(bullet, insPosition, rotation);


                        }
                        else
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);


                            rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                            Instantiate(bullet, insPosition, rotation);
                            rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                            Instantiate(bullet, insPosition, rotation);
                            rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                            Instantiate(bullet, insPosition, rotation);


                        }
                    }
                }
            }
            else if (currentWeapon is SemiAutoRifles)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (pm.isFacingRight)
                    {
                        var insPosition = new Vector3((transform.parent.position.x) + (transform.parent.localScale.x * transform.localPosition.x) + (transform.parent.localScale.x * front.localPosition.x * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * front.localPosition.x * transform.parent.localScale.y, 0);
                        Instantiate(bullet, insPosition, transform.rotation);
                    }
                    else
                    {
                        if (transform.rotation.eulerAngles.z <= 0)
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                            Instantiate(bullet, insPosition, transform.rotation);
                        }
                        else
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                            Instantiate(bullet, insPosition, transform.rotation);
                        }
                    }
                }
            }
        }
        else
        {
            if (currentWeapon is Rifles)
            {
                if (enemy.isFacingRight(transform.parent.transform.parent.transform))
                {
                    var insPosition = new Vector3((transform.parent.position.x) + (transform.parent.localScale.x * transform.localPosition.x) + (transform.parent.localScale.x * front.localPosition.x * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * front.localPosition.x * transform.parent.localScale.y, 0);
                    Instantiate(bullet, insPosition, transform.rotation);
                }
                else
                {
                    if (transform.rotation.eulerAngles.z <= 0)
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        Instantiate(bullet, insPosition, transform.rotation);
                    }
                    else
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        Instantiate(bullet, insPosition, transform.rotation);
                    }
                }
            }
            else if (currentWeapon is Shotguns)
            {
                float tempAngle;
                Quaternion rotation;
                tempAngle = transform.parent.transform.parent.transform.GetComponent<RangedEnemyCombatScript>().angle;

                if (enemy.isFacingRight(transform.parent.transform.parent.transform))
                {
                    Debug.Log("Shotgun SAG");
                    var insPosition = new Vector3((transform.parent.position.x) + (transform.parent.localScale.x * transform.localPosition.x) + (transform.parent.localScale.x * front.localPosition.x * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * front.localPosition.x * transform.parent.localScale.y, 0);


                    rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                    Instantiate(bullet, insPosition, rotation);
                    rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                    Instantiate(bullet, insPosition, rotation);
                    rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                    Instantiate(bullet, insPosition, rotation);


                }
                else
                {
                    Debug.Log("Shotgun SOL");
                    if (transform.rotation.eulerAngles.z <= 0)
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);

                        rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                        Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                        Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                        Instantiate(bullet, insPosition, rotation);


                    }
                    else
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);


                        rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                        Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                        Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                        Instantiate(bullet, insPosition, rotation);
                    }
                }
            }
            else if (currentWeapon is SemiAutoRifles)
            {
                if (enemy.isFacingRight(transform.parent.transform.parent))
                {
                    var insPosition = new Vector3((transform.parent.position.x) + (transform.parent.localScale.x * transform.localPosition.x) + (transform.parent.localScale.x * front.localPosition.x * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * front.localPosition.x * transform.parent.localScale.y, 0);
                    Instantiate(bullet, insPosition, transform.rotation);
                }
                else
                {
                    if (transform.rotation.eulerAngles.z <= 0)
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        Instantiate(bullet, insPosition, transform.rotation);
                    }
                    else
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        Instantiate(bullet, insPosition, transform.rotation);
                    }
                }
            }
        }

    }

    protected float getDamage()
    {
        return this.damage;
    }
    protected void setDamage(float dmg)
    {
        this.damage = dmg;
    }
    public string getName()
    {
        return this.gunName;
    }
    public void setName(string isim)
    {
        this.gunName = isim;
    }
    public int getAmmo()
    {
        return this.ammo;
    }
    public void setAmmo(int msayisi)
    {
        this.ammo = msayisi;
    }
    public int getStaticAmmo()
    {
        return this.staticAmmo;
    }
    public void setStaticAmmo()
    {
        this.staticAmmo = ammo;
    }
    public int getMags()
    {
        return this.mags;
    }
    public void setMags(int magsSayisi)
    {
        this.mags = magsSayisi;
    }
    public Sprite getSprite()
    {
        return this.sprite;
    }
    public void setSprite(Sprite sp)
    {
        this.sprite = sp;
    }
    public float getFireRate()
    {
        return this.fireRate;
    }
    public void setFireRate(float fr)
    {
        this.fireRate = fr;
    }
    public string getBulletPoints()
    {
        return this.bulletPoints;
    }
    public void setBulletPoints(string bulletPointName)
    {
        this.bulletPoints = bulletPointName;
    }
    public string getWeaponBullet()
    {
        return this.weaponBullet;
    }
    public void setWeaponBullet(string weaponBulletName)
    {
        this.weaponBullet = weaponBulletName;
    }

}

public class SemiAutoRifles : Weapons
{
    public SemiAutoRifles()
    {
        setFireRate(0.5f);
    }
}

public class Rifles : Weapons
{
    public Rifles()
    {
        setFireRate(0.2f);
    }
}

public class Shotguns : Weapons
{
    public Shotguns()
    {
        setFireRate(0.7f);
    }
}

/*
---------------------------Guns---------------------------
*/

public class Weapon1 : Rifles
{
    public Weapon1()
    {
        setName("Weapon1");
        setAmmo(30);
        setStaticAmmo();
        setMags(1);
        setSprite(Resources.Load<Sprite>("Sprites/Weapon1"));
        setBulletPoints("WEAPON1BULLETPOINTS");
        setWeaponBullet("Weapon1Bullet");
        setDamage(0);
    }
}

public class Weapon2 : SemiAutoRifles
{
    public Weapon2()
    {
        setName("Weapon2");
        setAmmo(30);
        setStaticAmmo();
        setMags(1);
        setSprite(Resources.Load<Sprite>("Sprites/Weapon2"));
        setBulletPoints("WEAPON2BULLETPOINTS");
        setWeaponBullet("Weapon2Bullet");
        setDamage(0);
    }
}

public class Weapon3 : SemiAutoRifles
{
    public Weapon3()
    {
        setName("Weapon3");
        setAmmo(30);
        setStaticAmmo();
        setMags(1);
        setSprite(Resources.Load<Sprite>("Sprites/Weapon3"));
        setBulletPoints("WEAPON3BULLETPOINTS");
        setWeaponBullet("Weapon3Bullet");
        setDamage(0);
    }
}

public class Weapon4 : Rifles
{
    public Weapon4()
    {
        setName("Weapon4");
        setAmmo(30);
        setStaticAmmo();
        setMags(1);
        setSprite(Resources.Load<Sprite>("Sprites/Weapon4"));
        setBulletPoints("WEAPON4BULLETPOINTS");
        setWeaponBullet("Weapon4Bullet");
        setDamage(0);
    }
}

public class Weapon5 : Shotguns
{
    public Weapon5()
    {
        setName("Weapon5");
        setAmmo(30);
        setStaticAmmo();
        setMags(1);
        setSprite(Resources.Load<Sprite>("Sprites/Weapon5"));
        setBulletPoints("WEAPON5BULLETPOINTS");
        setWeaponBullet("Weapon5Bullet");
        setDamage(0);
    }
}
