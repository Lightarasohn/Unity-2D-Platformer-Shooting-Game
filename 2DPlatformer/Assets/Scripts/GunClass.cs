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

    

    public void spawnGunPrefab(string gunName, Vector2 gunPosition, Quaternion gunRotation)
    {
        GameObject gun;
        gun = Resources.Load<GameObject>("Prefabs/GunSpawnPrefab");
        switch (gunName.ToUpper())
        {
            case "AK47":
                
                gun.transform.GetComponent<GunSpawnPrefabScript>().droppedWeapon = new AK47();
                Instantiate(gun, gunPosition, gunRotation);
                break;
            case "REVOLVER":
                
                gun.transform.GetComponent<GunSpawnPrefabScript>().droppedWeapon = new Revolver();
                Instantiate(gun, gunPosition, gunRotation);
                break;
            case "DEAGLE":
                
                gun.transform.GetComponent<GunSpawnPrefabScript>().droppedWeapon = new Deagle();
                Instantiate(gun, gunPosition, gunRotation);
                break;
            case "SCAR":
                
                gun.transform.GetComponent<GunSpawnPrefabScript>().droppedWeapon = new Scar();
                Instantiate(gun, gunPosition, gunRotation);
                break;
        }
    }

    public void reload()
    {
        if(mags > 0)
        {
            mags--;
            ammo = staticAmmo;
        }
        
    }

    public GameObject instantiateBulletSpawnPoints()
    {
        Transform transform = GameObject.FindGameObjectWithTag("PlayerGunPoint").transform;
        GameObject spawnpoints;
        string gun = GameObject.FindGameObjectWithTag("PlayerGunPoint").transform.GetComponent<GunPick>().currentWeapon.getName();
        switch (gun)
        {
            case "SCAR":
                spawnpoints = Resources.Load<GameObject>("WeaponsBulletSpawnPoints/SCARBULLETSPAWNPOINTS");
                Instantiate(spawnpoints, transform);
                break;
            case "AK47":
                spawnpoints = Resources.Load<GameObject>("WeaponsBulletSpawnPoints/AK47BULLETSPAWNPOINTS");
                Instantiate(spawnpoints, transform);
                break;
            case "REVOLVER":
                spawnpoints = Resources.Load<GameObject>("WeaponsBulletSpawnPoints/REVOLVERBULLETSPAWNPOINTS");
                Instantiate(spawnpoints, transform);
                break;
            case "DEAGLE":
                spawnpoints = Resources.Load<GameObject>("WeaponsBulletSpawnPoints/DEAGLEBULLETSPAWNPOINTS");
                Instantiate(spawnpoints, transform);
                break;
            default:
                spawnpoints = null;
                break;
        }
        return spawnpoints;
    }

    public void eraseOldBulletSpawnPoints()
    {
        var oldSpawnPoints = GameObject.FindGameObjectWithTag("BulletSpawnPointTag");
        GameObject.Destroy(oldSpawnPoints);
    }

    public void fire(GameObject spawnpoints)
    {
        Transform transform = GameObject.FindGameObjectWithTag("PlayerGunPoint").transform;
        Weapons currentWeapon = transform.GetComponent<GunPick>().currentWeapon;
        GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Transform front = spawnpoints.transform.GetChild(0);
        Transform back = spawnpoints.transform.GetChild(1);
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
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * back.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        Instantiate(bullet, insPosition, transform.rotation);
                    }
                    else
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * back.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        Instantiate(bullet, insPosition, transform.rotation);
                    }
                }
            }
        }
        else if (currentWeapon is Pistols)
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
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * back.localPosition.x * Mathf.Cos((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((transform.rotation.eulerAngles.z + 180) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        Instantiate(bullet, insPosition, transform.rotation);
                    }
                    else
                    {
                        var insPosition = new Vector3(transform.parent.position.x + transform.parent.localScale.x * transform.localPosition.x + transform.parent.localScale.x * back.localPosition.x * Mathf.Cos((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * -1, transform.parent.position.y + transform.parent.localScale.y * transform.localPosition.y + -1 * Mathf.Sin((180 - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad) * back.localPosition.x * transform.parent.localScale.y, 0);
                        Instantiate(bullet, insPosition, transform.rotation);
                    }
                }
            }
        }



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

}

public class Pistols : Weapons
{
    public Pistols()
    {
        setFireRate(0.3f);
    }
}

public class Rifles : Weapons
{
    public Rifles()
    {
        setFireRate(0.1f);
    }
}

/*
---------------------------Guns---------------------------
*/

public class Deagle : Pistols
{
    public Deagle()
    {
        setName("DEAGLE");
        setAmmo(7);
        setStaticAmmo();
        setMags(9999);
        setSprite(Resources.Load<Sprite>("Sprites/Deagle"));
    }
}

public class Revolver : Pistols
{
    public Revolver()
    {
        setName("REVOLVER");
        setAmmo(6);
        setStaticAmmo();
        setMags(9999);
        setSprite(Resources.Load<Sprite>("Sprites/Revolver"));
    }
}

public class AK47 : Rifles
{
    public AK47()
    {
        setName("AK47");
        setAmmo(30);
        setStaticAmmo();
        setMags(1);
        setSprite(Resources.Load<Sprite>("Sprites/AK47"));
    }
}

public class Scar : Rifles
{
    public Scar()
    {
        setName("SCAR");
        setAmmo(30);
        setStaticAmmo();
        setMags(1);
        setSprite(Resources.Load<Sprite>("Sprites/SCAR"));
    }
}
//
