using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPick : MonoBehaviour
{
    private Sprite oldSpriteRenderer;
    public Weapons currentWeapon;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentWeapon = new AK47();

        spriteRenderer.sprite = currentWeapon.getSprite();

        oldSpriteRenderer = transform.GetComponent<SpriteRenderer>().sprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
