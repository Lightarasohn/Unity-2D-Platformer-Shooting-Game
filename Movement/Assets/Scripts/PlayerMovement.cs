using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalMove;
    private int jumpCount = 0;
    private bool dash = false;
    private bool dashedInAir = false;
    public bool isDashing = false;
    public float dashPower = 2000f;
    public float dashTime = 0.2f;
    public float jumpingPower = 4f;
    public float moveSpeed = 16f;
    public bool isFacingRight = true;



    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private TrailRenderer tr;
    
    

    void Update()
    {
        if (isDashing) return;

        horizontalMove = Input.GetAxisRaw("Horizontal");
        if ((Input.GetButtonDown("Jump") && isGrounded()))
        {
            jumpCount++;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if(canDoubleJump())
        {
            jumpCount = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if (canDash())
        {
            dash = true;
        }

    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        
        flip();

        if (dash)
        {
            if (rb.velocity.y != 0) dashedInAir = true;
            StartCoroutine(DASH());
            dash = false;
        }
        else
            rb.velocity = new Vector2(horizontalMove * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);

    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
    }


    private bool canDoubleJump() 
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            return true;
        }
        else 
            return false;
    }

    private bool canDash()
    {
        if (isGrounded()) dashedInAir = false;
        return Input.GetButtonDown("Crouch") && horizontalMove != 0 && !dashedInAir;
    }

    private void flip()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vector = (mousePos - (Vector2)(transform.position));
        if ((isFacingRight && Math.Abs(vector.y)/vector.x<0) || (!isFacingRight && Math.Abs(vector.y)/vector.x > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
        }
    }


    


    private IEnumerator DASH()
    {
       
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(dashPower * horizontalMove, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
         
    }
}
