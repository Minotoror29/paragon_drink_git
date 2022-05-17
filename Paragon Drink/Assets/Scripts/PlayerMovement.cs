using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    #region Movement
    [Header("Movement")]
    private Vector2 direction;
    [SerializeField] private float speed;
    #endregion

    [SerializeField] private float jumpHeight;
    private bool jumpRegistered;
    private bool grounded = true;
    private bool canJump = true;
    [SerializeField, Range(0, 1)] private float groundNormalThreshold;
    private Transform currentGround;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float fallMultiplier;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && canJump)
        {
            jumpRegistered = true;
        }

        if (!grounded)
        {
            if (rb.velocity.y < 0f)
                rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            else if (!Input.GetButton("Jump"))
                rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        direction.x *= speed;
        direction.y = rb.velocity.y;

        if (jumpRegistered)
        {
            jumpRegistered = false;
            direction.y = Mathf.Sqrt(-2f * Physics2D.gravity.y * rb.gravityScale * (jumpHeight + 0.25f));
            canJump = false;
        }

        rb.velocity = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;
        if (normal.y > groundNormalThreshold)
        {
            canJump = true;
            grounded = true;
            currentGround = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == currentGround)
        {
            grounded = false;
            currentGround = null;
        }
    }
}
