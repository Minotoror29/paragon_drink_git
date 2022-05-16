using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 direction;
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        direction.x *= Time.fixedDeltaTime * speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
    }
}
