using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgroScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Transform castPoint;

    public float agroRange;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    Transform pointA;

    [SerializeField]
    Transform pointB;

    private Transform currentPoint;
    private Rigidbody2D rb;
    private bool isFacingLeft;
    private bool isAgro;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAgro)
        {
            voltaAt();
        }
        if(canSeePlayer(agroRange))
        {
            isAgro = true;
        }
        else
        {
            if (isAgro)
            {
                Invoke("takipEtmeyiBirak", 3);
            }
        }
        if(isAgro)
        {
            takipEt();
        }
    }
    
    void voltaAt()
    {
        if(currentPoint == pointA)
        {
            rb.velocity = new Vector2(moveSpeed, 1);
            isFacingLeft = false;
        }
        else
        {
            rb.velocity = new Vector2 (-moveSpeed, 1);
            isFacingLeft = true;
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA)
        {
            flip();
            currentPoint = pointB;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB)
        {
            flip();
            currentPoint = pointA;
        }
    }

    void takipEt()
    {
        if(transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed * 1.5f, 0);
            transform.localScale = new Vector2(1, 1);
            isFacingLeft = false;
        }
        else
        {
            rb.velocity = new Vector2 (-moveSpeed * 1.5f, 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        }
    }

    void takipEtmeyiBirak()
    {
        isAgro = false;
        rb.velocity = new Vector2(0, 0);
    }

    public bool canSeePlayer(float distance)
    {
        bool themp = false;
        float thempDistance = distance;

        if (isFacingLeft)
        {
            thempDistance = -distance;
        }

        Vector2 endPosition = castPoint.position + Vector3.right * thempDistance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPosition, 1 << LayerMask.NameToLayer("Action"));

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                themp = true;
            } 
            else
            {
                themp = false;
            }
        }
        return themp;
    }

    void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.position, 0.5f);
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
