using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAgroScript : MonoBehaviour
{
    [SerializeField]
    private Collider2D playerCollider;

    [SerializeField]
    private Collider2D enemyCollider;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform castPointer;

    [SerializeField]
    float agroRange;

    private float agresiveAgroRange;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    Transform pointA;

    [SerializeField]
    Transform pointB;

    private Transform currentPoint;

    //float playerDistance;

    private bool isFacingLeft = false;
    private bool isAgro = false;
    private bool isSearching = false;

    Rigidbody2D rb2d;
    private Vector2 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentPoint = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        /*   ENEMY PATH (VOLTA ATIYO PLATFORM BOYUNCA)   */
        if (!isAgro)
        {
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA)
            {
                currentPoint = pointB;
                flip();
            }
            else if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB)
            {
                currentPoint = pointA;
                flip();
            }

            if (currentPoint == pointA)
            {
                rb2d.velocity = new Vector2(moveSpeed, 0);
                isFacingLeft = false;
            }
            else
            {
                rb2d.velocity = new Vector2(-moveSpeed, 0);
                isFacingLeft = true;
            }
        }
    }

    private void FixedUpdate()
    {
        /*    TAKÝP KODU     */

        //playerDistance = Vector2.Distance(transform.position, player.position);
        //playerDistance = Physics2D.Distance(playerCollider, enemyCollider).distance;


        if (canSeePlayer(agroRange))
        {
            // AGRO ENEMY
            isAgro = true;
        }
        else
        {
            if (isAgro)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    //5 saniye delaylý
                    Invoke("stopChaseToPlayer", 5);
                }
            }
        }

        if (isAgro)
        {
            chaseToPlayer();
        }
    }


    private void chaseToPlayer()
    {

        //Enemy sað taraftaysa
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed * (1.5f), 0);
            transform.localScale = new Vector2(1, 1);
            isFacingLeft = false;
        }
        //Enemy soldaysa
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed * (1.5f), 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        }
    }

    private void stopChaseToPlayer()
    {
        isSearching = false;
        isAgro = false;
        rb2d.velocity = new Vector2(0, 0);
    }

    bool canSeePlayer(float distance)
    {
        bool themVal = false;
        float castDistance = distance;

        if (!isFacingLeft)
        {
            endPosition = castPointer.position + Vector3.right * castDistance;
        }
        else
        {
            endPosition = castPointer.position + Vector3.left * castDistance;
        }



        RaycastHit2D hit = Physics2D.Linecast(castPointer.position, endPosition, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("PlayerTag"))
            {
                themVal = true;
            }
            else
            {
                themVal = false;
            }
        }
        return themVal;
    }

    private void flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.position, 0.5f);
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
