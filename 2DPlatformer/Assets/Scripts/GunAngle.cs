using UnityEngine;

public class GunAngle : MonoBehaviour
{
    
    private SpriteRenderer sr;
    private SpriteRenderer srParent;
    private PlayerMovement pm;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        srParent = transform.parent.transform.GetComponent<SpriteRenderer>();
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
        if(!GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead())
        {Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vector = (mousePos - (Vector2)(transform.parent.position));
        angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.parent.rotation = rotation;

            if (!pm.isFacingRight)
            {
                if (!sr.flipX)
                {
                    srParent.flipX = true;
                    srParent.flipY = true;
                    sr.flipX = true;
                    sr.flipY = true;
                }
            }
            else
            {
                srParent.flipX = false;
                srParent.flipY = false;
                sr.flipY = false;
                sr.flipX = false;
            }
        }
        
        
    }

}
