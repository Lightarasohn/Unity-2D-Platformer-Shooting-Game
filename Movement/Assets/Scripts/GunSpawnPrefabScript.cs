using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawnPrefabScript : MonoBehaviour
{
    
    public Weapons droppedWeapon;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = droppedWeapon.getSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
