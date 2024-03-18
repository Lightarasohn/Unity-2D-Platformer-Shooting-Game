using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPick : MonoBehaviour
{
    private Sprite oldSpriteRenderer;
    public Weapons currentWeapon;
    private SpriteRenderer spriteRenderer;
    public bool gunPicked = false;
    public string gunPickedName;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentWeapon = new Scar();

        spriteRenderer.sprite = currentWeapon.getSprite();

        oldSpriteRenderer = transform.GetComponent<SpriteRenderer>().sprite;

    }

    // Update is called once per frame
    void Update()
    {
        if (gunPicked)
        {
            currentWeapon.eraseOldBulletSpawnPoints();
            switch (gunPickedName.ToUpper())
            {
                case "DEAGLE":
                    currentWeapon = new Deagle();
                    break;
                case "REVOLVER":
                    currentWeapon = new Revolver();
                    break;
                case "SCAR":
                    currentWeapon = new Scar();
                    break;
                case "AK47":
                    currentWeapon = new AK47();
                    break;
            }
            spriteRenderer.sprite = currentWeapon.getSprite();

            oldSpriteRenderer = transform.GetComponent<SpriteRenderer>().sprite;
            gunPicked = false;
        }
    }
}
