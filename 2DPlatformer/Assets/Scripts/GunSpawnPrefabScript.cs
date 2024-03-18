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
        transform.GetComponent<SpriteRenderer>().sprite = droppedWeapon.getSprite();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Input.GetButtonDown("E"))
        {
            GameObject.Destroy(gameObject);
            GameObject.FindGameObjectWithTag("PlayerGunPoint").transform.GetComponent<GunPick>().gunPicked = true;
            GameObject.FindGameObjectWithTag("PlayerGunPoint").transform.GetComponent<GunPick>().gunPickedName = droppedWeapon.getName();
        }
    }
}
