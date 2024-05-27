using UnityEngine;

public class GunPick : MonoBehaviour
{
    public Weapons currentWeapon;
    private SpriteRenderer spriteRenderer;
    public bool isGunPicked = false;
    public Weapons pickedGun;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentWeapon = new Weapon2(false);
        spriteRenderer.sprite = currentWeapon.getSprite();

    }

    // Update is called once per frame
    void Update()
    {
        if (isGunPicked)
        {
            isGunPicked = false;
            currentWeapon.eraseOldBulletSpawnPoints();
            currentWeapon = pickedGun;
            currentWeapon.reArrangeBulletType();
            spriteRenderer.sprite = currentWeapon.getSprite();
            currentWeapon.instantiateBulletSpawnPoints(transform);
        }
    }
}
