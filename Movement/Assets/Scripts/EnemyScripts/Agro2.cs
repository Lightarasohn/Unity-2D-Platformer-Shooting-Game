using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agro2 : MonoBehaviour
{
    [SerializeField]
    private GameObject pointA;

    [SerializeField]
    private GameObject pointB;

    [SerializeField]
    private Transform castPionter;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float agroRange;

    [SerializeField]
    private GameObject player;

    private Rigidbody2D rb;
    private Transform currentPoint;

    //Boollean field
    private bool isFacingLeft = false;
    private bool isAgro = false;
    private bool isSearching = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointA.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        if (canSeePlayer(agroRange))
        {
            isAgro = true;
        }
        else
        {
            if (isAgro)
            {
                if(!isSearching)
                {
                    isSearching = true;
                    Invoke("takipEtme", 3);
                }
            }
        }
        if (isAgro)
        {
            takipEt();
        }
    }
    private void volta()
    {
        if (currentPoint == pointA.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.7f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.7f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
    }

    private bool canSeePlayer(float distance)
    {
        bool tmp = false;
        float castDist;

        if (isFacingLeft)
        {
            castDist = -distance;
        }
        else
        {
            castDist = distance;
        }

        Vector2 endPosition = castPionter.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPionter.position, endPosition, 1 << LayerMask.NameToLayer("ActionLayer"));

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                tmp = true;
            }
            else
            {
                tmp = false;
            }
        }
        return tmp;
    }


    private void flip()
    {
        Vector3 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;
    }

    
    private void takipEt()
    {
        if(transform.position.x < player.transform.position.x)
        {
            //Karakter saðda ise
            rb.velocity = new Vector2(speed * 1.7f, 0);
            transform.localScale = new Vector2(1, 1);
            isFacingLeft = false;
        }
        else
        {
            rb.velocity = new Vector2(-speed * 1.7f, 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        }
    }

    private void takipEtme()
    {
        isAgro = false;
        isSearching = false;
        rb.velocity = new Vector2(0, 0);
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
