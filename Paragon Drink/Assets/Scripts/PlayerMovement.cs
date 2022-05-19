using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    #region Movement
    [Header("Movement")]
    private Vector2 direction;
    [SerializeField] private float speed;
    [HideInInspector] public bool canControl = true;
    #endregion

    [HideInInspector] public float jumpHeight;
    private bool jumpRegistered;
    private bool grounded = true;
    private bool canJump = true;
    [SerializeField, Range(0, 1)] private float groundNormalThreshold;
    private Transform currentGround;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float fallMultiplier;
    [HideInInspector] public bool canFallJump;

    private bool canCoyoteJump = true;
    [SerializeField] private float coyoteJumpTime;
    private float cjTimer = 0;

    private void Start()
    {
        canControl = true;
    }

    private void Update()
    {
        if (canControl)
        {
            direction.x = Input.GetAxisRaw("Horizontal");

            if (direction.x != 0)
            {
                if (direction.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (direction.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (Input.GetButtonDown("Jump") && canJump)
            {
                jumpRegistered = true;
            }

            if (!grounded)
            {
                if (rb.velocity.y < 0f)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
                }
                else if (!Input.GetButton("Jump"))
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
                }

                if (canCoyoteJump)
                {
                    if (cjTimer > 0)
                    {
                        cjTimer -= Time.deltaTime;
                    }
                    else
                    {
                        canCoyoteJump = false;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (canControl)
        {
            direction.x *= speed;
            direction.y = rb.velocity.y;

            if (jumpRegistered)
            {
                if (grounded || canFallJump || canCoyoteJump)
                {
                    jumpRegistered = false;
                    direction.y = Mathf.Sqrt(-2f * Physics2D.gravity.y * rb.gravityScale * (jumpHeight + 0.25f));
                    canJump = false;
                }
            }

            rb.velocity = direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;
        if (normal.y > groundNormalThreshold)
        {
            canJump = true;
            canCoyoteJump = true;
            grounded = true;
            currentGround = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == currentGround)
        {
            cjTimer = coyoteJumpTime;
            grounded = false;
            currentGround = null;
        }
    }
}
