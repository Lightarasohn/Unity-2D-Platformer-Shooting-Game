using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrefabMovement : MonoBehaviour
{
    public float bulletspeed = 50f;
    private Rigidbody2D bulletRigidbody;
    private float bulletVelocityX;
    private float bulletVelocityY;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = transform.GetComponent<Rigidbody2D>();
        var bulletRotation = transform.rotation.eulerAngles.z;
        bulletVelocityX = Mathf.Cos(bulletRotation * Mathf.Deg2Rad) * bulletspeed;
        bulletVelocityY = Mathf.Sin(bulletRotation * Mathf.Deg2Rad) * bulletspeed;
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidbody.velocity = new Vector2(bulletVelocityX, bulletVelocityY);
    }
}
