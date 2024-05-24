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
        currentWeapon = new Weapon5();
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
            spriteRenderer.sprite = currentWeapon.getSprite();
            currentWeapon.instantiateBulletSpawnPoints(transform);
        }
    }
}
