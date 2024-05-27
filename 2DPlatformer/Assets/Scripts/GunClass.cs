using System.Diagnostics;
using UnityEngine;




public class Weapons
{
    protected string gunName;
    protected int ammo;
    protected int staticAmmo;
    protected int mags;
    protected Sprite sprite;
    protected float fireRate;
    protected string bulletPoints;
    protected string weaponBullet;
    protected AudioClip weaponSound;
    protected bool isEnemy;

    protected void setBulletType()
    {
        if (this.isEnemy)
        {
            this.weaponBullet += "(Enemy)";
        }
    }
    public void reArrangeBulletType()
    {
        this.weaponBullet = this.weaponBullet.Replace("(Enemy)", "");
        this.isEnemy = false;
    }
   public string getName()
   {
       return this.gunName;
   }
   public int getAmmo()
   {
       return this.ammo;
   }
   public int getStaticAmmo()
   {
       return this.staticAmmo;
   }
   public int getMags()
   {
       return this.mags;
   }
   public string getBulletPoints()
   {
       return this.bulletPoints;
   }
   public string getWeaponBullet()
   {
       return this.weaponBullet;
   }
   public Sprite getSprite()
    {
        return this.sprite;
    }
   public float getFireRate()
    {
        return this.fireRate;
    }

    public void spawnGunPrefab(Weapons gunType, Vector2 gunPosition, Quaternion gunRotation)
    {
        GameObject gun, instantiedGun;
        gunPosition.Set(gunPosition.x, gunPosition.y - 0.7f);
        gun = Resources.Load<GameObject>("Prefabs/RuntimePrefabs/GunSpawnPrefab");
        instantiedGun = GameObject.Instantiate(gun, gunPosition, gunRotation);
        instantiedGun.transform.GetComponent<GunSpawnPrefabScript>().droppedWeapon = gunType;
    }
    /*
    public void reload()
    {
        if(mags > 0)
        {
            mags--;
            ammo = staticAmmo;
        }
        
    }
    */

    public GameObject instantiateBulletSpawnPoints(Transform transform)
    {
        GameObject spawnpoints;
        spawnpoints = Resources.Load<GameObject>("WeaponsBulletSpawnPoints/" + this.bulletPoints);
        
        GameObject.Instantiate(spawnpoints, transform);

        return spawnpoints;
    }
    public void eraseOldBulletSpawnPoints()
    {
        var oldSpawnPoints = GameObject.FindGameObjectsWithTag("BulletSpawnPointTag");
        foreach (var spawnPoint in oldSpawnPoints)
        {
            GameObject.Destroy(spawnPoint);
        }
    }
    public void fire(GameObject spawnpoints, Transform transform, Weapons currentWeapon)
    {
        GameObject bullet = Resources.Load<GameObject>("Prefabs/RuntimePrefabs/" + currentWeapon.weaponBullet);
        PlayerMovement pm;
        EnemyClass enemy;
        bool isShot = false;
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
                        GameObject.Instantiate(bullet, insPosition, transform.rotation);
                        isShot = true;
                    }
                    else
                    {
                        if (transform.rotation.eulerAngles.z <= 0)
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                            GameObject.Instantiate(bullet, insPosition, transform.rotation);
                            isShot = true;
                        }
                        else
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                            GameObject.Instantiate(bullet, insPosition, transform.rotation);
                            isShot = true;
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
                        
                        var insPosition = new Vector3((transform.parent.position.x) + (transform.parent.localScale.x * transform.localPosition.x) + (transform.parent.localScale.x * front.localPosition.x * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * front.localPosition.x * transform.parent.localScale.y, 0);


                        rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                        GameObject.Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                        GameObject.Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                        GameObject.Instantiate(bullet, insPosition, rotation);
                        isShot = true;

                    }
                    else
                    {
                        
                        if (transform.rotation.eulerAngles.z <= 0)
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);

                            rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                            GameObject.Instantiate(bullet, insPosition, rotation);
                            rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                            GameObject.Instantiate(bullet, insPosition, rotation);
                            rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                            GameObject.Instantiate(bullet, insPosition, rotation);
                            isShot = true;

                        }
                        else
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);


                            rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                            GameObject.Instantiate(bullet, insPosition, rotation);
                            rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                            GameObject.Instantiate(bullet, insPosition, rotation);
                            rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                            GameObject.Instantiate(bullet, insPosition, rotation);
                            isShot = true;

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
                        GameObject.Instantiate(bullet, insPosition, transform.rotation);
                        isShot = true;
                    }
                    else
                    {
                        if (transform.rotation.eulerAngles.z <= 0)
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                            GameObject.Instantiate(bullet, insPosition, transform.rotation);
                            isShot = true;
                        }
                        else
                        {
                            var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                            GameObject.Instantiate(bullet, insPosition, transform.rotation);
                            isShot = true;
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
                    GameObject.Instantiate(bullet, insPosition, transform.rotation);
                    isShot = true;
                }
                else
                {
                    if (transform.rotation.eulerAngles.z <= 0)
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        GameObject.Instantiate(bullet, insPosition, transform.rotation);
                        isShot = true;
                    }
                    else
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        GameObject.Instantiate(bullet, insPosition, transform.rotation);
                        isShot = true;
                    }
                }
            }
            else if (currentWeapon is Shotguns)
            {
                float tempAngle;
                Quaternion rotation;
                tempAngle = transform.GetComponent<RangedEnemyCombatScript>().angle;

                if (enemy.isFacingRight(transform.parent.transform.parent.transform))
                {
                    
                    var insPosition = new Vector3((transform.parent.position.x) + (transform.parent.localScale.x * transform.localPosition.x) + (transform.parent.localScale.x * front.localPosition.x * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * front.localPosition.x * transform.parent.localScale.y, 0);


                    rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                    GameObject.Instantiate(bullet, insPosition, rotation);
                    rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                    GameObject.Instantiate(bullet, insPosition, rotation);
                    rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                    GameObject.Instantiate(bullet, insPosition, rotation);
                    isShot = true;

                }
                else
                {
                    
                    if (transform.rotation.eulerAngles.z <= 0)
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);

                        rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                        GameObject.Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                        GameObject.Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                        GameObject.Instantiate(bullet, insPosition, rotation);
                        isShot = true;

                    }
                    else
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);


                        rotation = Quaternion.AngleAxis(tempAngle, Vector3.forward);
                        GameObject.Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle + 5, Vector3.forward);
                        GameObject.Instantiate(bullet, insPosition, rotation);
                        rotation = Quaternion.AngleAxis(tempAngle - 5, Vector3.forward);
                        GameObject.Instantiate(bullet, insPosition, rotation);
                        isShot = true;
                    }
                }
            }
            else if (currentWeapon is SemiAutoRifles)
            {
                if (enemy.isFacingRight(transform.parent.transform.parent))
                {
                    var insPosition = new Vector3((transform.parent.position.x) + (transform.parent.localScale.x * transform.localPosition.x) + (transform.parent.localScale.x * front.localPosition.x * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * front.localPosition.x * transform.parent.localScale.y, 0);
                    GameObject.Instantiate(bullet, insPosition, transform.rotation);
                    isShot = true;
                }
                else
                {
                    if (transform.rotation.eulerAngles.z <= 0)
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad), transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        GameObject.Instantiate(bullet, insPosition, transform.rotation);
                        isShot = true;
                    }
                    else
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * front.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        GameObject.Instantiate(bullet, insPosition, transform.rotation);
                        isShot = true;
                    }
                }
            }
        }
        if (isShot)
        {
            AudioSource audioSource = transform.GetComponent<AudioSource>();
            audioSource.clip = currentWeapon.weaponSound;
            audioSource.Play();
        }
    }
   
}

public class SemiAutoRifles : Weapons
{
    protected SemiAutoRifles()
    {
        base.fireRate = 0.7f;
    }
}

public class Rifles : Weapons
{
    protected Rifles()
    {
        base.fireRate = 0.2f;
    }
}

public class Shotguns : Weapons
{
    protected Shotguns()
    {
        base.fireRate = 0.7f;
    }
}

/*
---------------------------Guns---------------------------
*/

public class Weapon1 : Rifles
{
    public Weapon1(bool isEnemy)
    {
        base.isEnemy = isEnemy;
        base.gunName = "Weapon1";
        base.ammo = 30;
        base.staticAmmo = ammo;
        base.mags = 1;
        base.sprite = Resources.Load<Sprite>("Sprites/Weapon1");
        base.bulletPoints = "WEAPON1BULLETPOINTS";
        base.weaponBullet = "Weapon1Bullet";
        base.weaponSound = Resources.Load<AudioClip>("SoundEffects/GameAndMenuSounds/WeaponSounds/" + this.gunName);
        base.setBulletType();
    }
}

public class Weapon2 : SemiAutoRifles
{
    public Weapon2(bool isEnemy)
    {
        base.isEnemy = isEnemy;
        base.gunName = "Weapon2";
        base.ammo = 20;
        base.staticAmmo = ammo;
        base.mags = 1;
        base.sprite = Resources.Load<Sprite>("Sprites/Weapon2");
        base.bulletPoints = "WEAPON2BULLETPOINTS";
        base.weaponBullet = "Weapon2Bullet";
        base.weaponSound = Resources.Load<AudioClip>("SoundEffects/GameAndMenuSounds/WeaponSounds/" + this.gunName);
        base.setBulletType();
    }
}

public class Weapon3 : SemiAutoRifles
{
    public Weapon3(bool isEnemy)
    {
        base.isEnemy = isEnemy;
        base.gunName = "Weapon3";
        base.ammo = 35;
        base.staticAmmo = ammo;
        base.mags = 1;
        base.sprite = Resources.Load<Sprite>("Sprites/Weapon3");
        base.bulletPoints = "WEAPON3BULLETPOINTS";
        base.weaponBullet = "Weapon3Bullet";
        base.weaponSound = Resources.Load<AudioClip>("SoundEffects/GameAndMenuSounds/WeaponSounds/"  + this.gunName);
        base.setBulletType();
    }
}

public class Weapon4 : Rifles
{
    public Weapon4(bool isEnemy)
    {
        base.isEnemy = isEnemy;
        base.gunName = "Weapon4";
        base.ammo = 25;
        base.staticAmmo = ammo;
        base.mags = 1;
        base.sprite = Resources.Load<Sprite>("Sprites/Weapon4");
        base.bulletPoints = "WEAPON4BULLETPOINTS";
        base.weaponBullet = "Weapon4Bullet";
        base.weaponSound = Resources.Load<AudioClip>("SoundEffects/GameAndMenuSounds/WeaponSounds/" + this.gunName);
        base.setBulletType();
    }
}

public class Weapon5 : Shotguns
{
    public Weapon5(bool isEnemy)
    {
        base.isEnemy = isEnemy;
        base.gunName = "Weapon5";
        base.ammo = 12;
        base.staticAmmo = ammo;
        base.mags = 1;
        base.sprite = Resources.Load<Sprite>("Sprites/Weapon5");
        base.bulletPoints = "WEAPON5BULLETPOINTS";
        base.weaponBullet = "Weapon5Bullet";
        base.weaponSound = Resources.Load<AudioClip>("SoundEffects/GameAndMenuSounds/WeaponSounds/" + this.gunName);
        base.setBulletType();
    }
}
