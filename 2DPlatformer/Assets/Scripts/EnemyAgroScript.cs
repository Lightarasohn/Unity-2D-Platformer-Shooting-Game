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

    private Rigidbody2D rb;

    private bool isFacingLeft;
    private bool isAgro;
    private float timer;
    private bool moveRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAgro)
        {
            voltaAt();
        }
        if (canSeePlayer(agroRange))
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
        if (isAgro)
        {
            takipEt();
        }
    }

    void voltaAt()
    {
        if (timer > 4)
        {
            moveRight = !moveRight;
            timer = 0;
        }

        if (moveRight)
        {
            rb.velocity = new Vector2(moveSpeed, 1);
            transform.localScale = new Vector2(1, 1);
            isFacingLeft = false;
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 1);
            transform.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        }

        timer += Time.deltaTime;
    }

    void takipEt()
    {
        if (transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed * 1.5f, 0);
            transform.localScale = new Vector2(1, 1);
            isFacingLeft = false;
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed * 1.5f, 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        }
    }

    void takipEtmeyiBirak()
    {
        isAgro = false;
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

        if (hit.collider != null)
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
}
