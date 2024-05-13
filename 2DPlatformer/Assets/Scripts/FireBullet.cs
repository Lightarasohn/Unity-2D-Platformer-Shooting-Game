using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBullet : MonoBehaviour
{
    private string gun;
    private GameObject spawnpoints;
    private float time = 0.5f;
    private float semiTime = 10f;
    private float fireRate;
    private Weapons currentWeapon;
    private string oldGun;
    // Start is called before the first frame update
    void Start()
    {
        
        currentWeapon = transform.GetComponent<GunPick>().currentWeapon;
        fireRate = transform.GetComponent<GunPick>().currentWeapon.getFireRate();
        gun = transform.GetComponent<GunPick>().currentWeapon.getName();
        oldGun = "";
    }

    // Update is called once per frame
    void Update()
    {
        gun = transform.GetComponent<GunPick>().currentWeapon.getName();
        if (gun != oldGun)
        {
            spawnpoints = currentWeapon.instantiateBulletSpawnPoints(transform);
        }
        semiTime += Time.deltaTime;
        if (currentWeapon is Shotguns || currentWeapon is SemiAutoRifles)
        {
            if (semiTime >= fireRate)
            {
                currentWeapon.fire(spawnpoints,transform);
                if (Input.GetMouseButtonDown(0))
                {
                    semiTime = 0;
                }
            }
        }


        oldGun = gun;

        


    }
    private void FixedUpdate()
    {
        
        
        time += Time.deltaTime;
        if(currentWeapon is Rifles)
        {
            if (time >= fireRate)
            {
                currentWeapon.fire(spawnpoints,transform);
                time = 0;
            }
        }
        
    }

}
