using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBullet : MonoBehaviour
{
    private string gun;
    private GameObject spawnpoints;
    private PlayerMovement pm;
    private GameObject bullet;
    private Transform front;
    private Transform back;
    private float time = 0.5f;
    private float fireRate;
    private Weapons currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = transform.GetComponent<GunPick>().currentWeapon;
        fireRate = transform.GetComponent<GunPick>().currentWeapon.getFireRate();
        pm = transform.GetComponentInParent<PlayerMovement>();
        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if(gun ==  null)
        {
            gun = transform.GetComponent<GunPick>().currentWeapon.getName();
            instantiateBulletSpawnPoints();
            front = spawnpoints.transform.GetChild(0);
            back = spawnpoints.transform.GetChild(1);
        }
        if(currentWeapon is Pistols)
        {
            fire();
        }
        
    }
    private void FixedUpdate()
    {
        
        
        time += Time.deltaTime;
        if(currentWeapon is Rifles)
        {
            if (time >= fireRate)
            {
                fire();
                time = 0;
            }
        }  
    }

    private void eraseOldBulletSpawnPoints()
    {
        var oldSpawnPoints = GameObject.FindGameObjectWithTag("BulletSpawnPointTag");
        GameObject.Destroy(oldSpawnPoints);
    }

    private void instantiateBulletSpawnPoints()
    {
        switch (gun)
        {
            case "SCAR":
                spawnpoints = Resources.Load<GameObject>("WeaponsBulletSpawnPoints/SCARBULLETSPAWNPOINTS");
                Instantiate(spawnpoints, transform);
                break;
            case "AK-47":
                spawnpoints = Resources.Load<GameObject>("WeaponsBulletSpawnPoints/AK47BULLETSPAWNPOINTS");
                Instantiate(spawnpoints, transform);
                break;
            case "Revolver":
                spawnpoints = Resources.Load<GameObject>("WeaponsBulletSpawnPoints/REVOLVERBULLETSPAWNPOINTS");
                Instantiate(spawnpoints, transform);
                break;
            case "Deagle":
                spawnpoints = Resources.Load<GameObject>("WeaponsBulletSpawnPoints/DEAGLEBULLETSPAWNPOINTS");
                Instantiate(spawnpoints, transform);
                break;

        }
    }

    private void fire()
    {
        if(currentWeapon is Rifles)
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
        else if(currentWeapon is Pistols)
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

}
