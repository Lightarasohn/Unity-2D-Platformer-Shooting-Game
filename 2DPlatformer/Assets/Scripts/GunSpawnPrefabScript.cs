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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetButtonDown("E"))
            {
                GameObject.FindGameObjectWithTag("PlayerGunPoint").transform.GetComponent<GunPick>().isGunPicked = true;
                GameObject.FindGameObjectWithTag("PlayerGunPoint").transform.GetComponent<GunPick>().pickedGun = droppedWeapon;
                GameObject.Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetButtonDown("E"))
            {
                GameObject.FindGameObjectWithTag("PlayerGunPoint").transform.GetComponent<GunPick>().isGunPicked = true;
                GameObject.FindGameObjectWithTag("PlayerGunPoint").transform.GetComponent<GunPick>().pickedGun = droppedWeapon;
                GameObject.Destroy(gameObject);
            }
        }
    }
}
