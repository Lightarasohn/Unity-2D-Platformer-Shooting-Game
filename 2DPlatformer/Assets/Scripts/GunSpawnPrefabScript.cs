using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawnPrefabScript : MonoBehaviour
{
    
    public Weapons droppedWeapon;
    public string gunName;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetButtonDown("E"))
        {
            GameObject.FindGameObjectWithTag("PlayerGunPoint").transform.GetComponent<GunPick>().isGunPicked = true;
            GameObject.FindGameObjectWithTag("PlayerGunPoint").transform.GetComponent<GunPick>().pickedGun= droppedWeapon;
            GameObject.Destroy(gameObject);
        }
    }
}
