using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunAngle : MonoBehaviour
{
    
    private SpriteRenderer sr;
    private PlayerMovement pm;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
        pm = transform.GetComponentInParent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateGun();

    }

    private void rotateGun()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vector = (mousePos - (Vector2)(transform.position));
        angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;


        if (!pm.isFacingRight)
        {
            if (!sr.flipX)
            {
                sr.flipX = true;
                sr.flipY = true;
            }
        }
        else
        {
            sr.flipY = false;
            sr.flipX = false;
        }
        
        
    }

}
