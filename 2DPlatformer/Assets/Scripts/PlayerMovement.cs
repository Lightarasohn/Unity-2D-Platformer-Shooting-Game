using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isFacingRight = true;
    public bool isDashing = false;
    private float horizontalMove;
    private int jumpCount = 0;
    private bool dash = false;
    private bool dashedInAir = false;
    private float dashPower = 40f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;
    private float dashTimer = 2f;
    public float jumpingPower = 15f;
    private float moveSpeed = 500f;
    private Animator animator;

    private Rigidbody2D rb;
    private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    private TrailRenderer tr;
    private PlayerSoundEffects playerSoundEffects;
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        GroundCheck = transform.GetChild(0);
        tr = transform.GetComponent<TrailRenderer>();
        animator = transform.GetComponent<Animator>();
        playerSoundEffects = transform.GetComponent<PlayerSoundEffects>();
    }

    void Update()
    {
        if (isDashing) return;
        dashTimer += Time.deltaTime;

        horizontalMove = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if ((Input.GetButtonDown("Jump") && isGrounded()))
        {
            jumpCount++;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetBool("isJumping", true);
            playerSoundEffects.playJumpSoundEffect();
        }
        else if (canDoubleJump())
        {
            jumpCount = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            playerSoundEffects.playJumpSoundEffect();
        }
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Zýplama animasyonunu sýfýrla
        if (Math.Round(rb.velocity.y) == 0 && isGrounded())
        {
            animator.SetBool("isJumping", false);
            if (Mathf.Abs(horizontalMove) > 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            }
        }

        if (canDash())
        {
            dash = true;
            dashTimer = 0;
        }

        // Silahýn pozisyonu güncelleniyor
        UpdateWeaponPosition();
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
        {
            rb.velocity = new Vector2(horizontalMove * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
    }

    public bool isGrounded()
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
        return Input.GetButtonDown("Crouch") && horizontalMove != 0 && !dashedInAir && dashTimer >= dashCooldown;
    }

    private void flip()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vector = (mousePos - (Vector2)(transform.position));
        if ((isFacingRight && Math.Abs(vector.y) / vector.x < 0) || (!isFacingRight && Math.Abs(vector.y) / vector.x > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
        }
    }

    private void UpdateWeaponPosition()
    {
        if (isGrounded() && Math.Round(rb.velocity.x) != 0)
        {
            transform.GetChild(2).transform.localPosition = new Vector3(1.2f, transform.GetChild(2).transform.localPosition.y, transform.GetChild(2).transform.localPosition.z);
        }
        else if (!isGrounded())
        {
            transform.GetChild(2).transform.localPosition = new Vector3(1.25f, 0.25f, transform.GetChild(2).transform.localPosition.z);
        }
        else
        {
            transform.GetChild(2).transform.localPosition = new Vector3(0.8f, 0, 0);
        }
    }

    private IEnumerator DASH()
    {
        isDashing = true;
        playerSoundEffects.playDashSoundEffect();
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
