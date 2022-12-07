using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewCharacterController : MonoBehaviour
{
    private PlayerControls _playerControls;

    private Rigidbody2D _rb;

    private Animator _anim;

    [Header("Movement")]
    [SerializeField] private float speed = 1f;
    private Vector2 _direction;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 1f;
    private bool _jumpRegistered = false;
    private bool _grounded = false;
    [SerializeField] private float groundNormalThreshold = 0.6f;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _playerControls = new PlayerControls();
        _playerControls.Movement.Enable();
        _playerControls.Movement.Jump.performed += ctx => Jump();

        _rb = GetComponent<Rigidbody2D>();

        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _direction.x = _playerControls.Movement.Movement.ReadValue<Vector2>().x;

        if (_direction.x != 0)
        {
            if (_direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (_direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (_direction.x == 0)
        {
            _anim.SetBool("isRunning", false);
        }
        else
        {
            _anim.SetBool("isRunning", true);
        }

        if (!_grounded)
        {
            if (_rb.velocity.y < 0f)
            {
                _anim.SetBool("isFalling", true);
            }
        }
    }

    private void FixedUpdate()
    {
        _direction.x *= speed;
        _direction.y = _rb.velocity.y;

        if (_jumpRegistered)
        {
            if (_grounded)
            {
                _jumpRegistered = false;
                _direction.y = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rb.gravityScale * (jumpHeight + 0.25f));
                _anim.SetTrigger("Jump");
                _anim.SetBool("isFalling", false);
            }
        }

        _rb.velocity = _direction;
    }

    private void Jump()
    {
        _jumpRegistered = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;
        if (normal.y > groundNormalThreshold)
        {
            _grounded = true;
            _anim.SetBool("isGrounded", true);
            _anim.SetBool("isFalling", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _grounded = false;
        _anim.SetBool("isGrounded", false);
    }
}
