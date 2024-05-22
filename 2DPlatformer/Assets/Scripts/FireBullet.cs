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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead() && Time.timeScale == 1)
        {
            currentWeapon = transform.GetComponent<GunPick>().currentWeapon;
        fireRate = currentWeapon.getFireRate();
        if (spawnpoints == null)
        {
            spawnpoints = currentWeapon.instantiateBulletSpawnPoints(transform);
        }
        
        semiTime += Time.deltaTime;
            if (currentWeapon is Shotguns || currentWeapon is SemiAutoRifles)
            {
                if (semiTime >= fireRate)
                {

                    if (Input.GetMouseButtonDown(0))
                    {
                        currentWeapon.fire(spawnpoints, transform, currentWeapon);
                        semiTime = 0;
                    }
                }
            }
        }

    }
    private void FixedUpdate()
    {

        if (!GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead() && Time.timeScale == 1)
        {
            time += Time.deltaTime;
            if (currentWeapon is Rifles)
            {
                if (time >= fireRate)
                {
                    currentWeapon.fire(spawnpoints, transform, currentWeapon);
                    time = 0;
                }
            }
        }
        
    }

}
