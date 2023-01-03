using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance;
    public static PlayerMovement Instance => instance;

    public Rigidbody2D rb;

    [Header("Movement")]
    public float speed;
    private Vector2 _direction;
    [HideInInspector] public bool canControl = true;

    [Header("Jump")]
    public float jumpHeight;
    private bool _jumpRegistered;
    private bool _jumpInput;
    public bool _grounded = true;
    private bool _canJump = true;
    [SerializeField, Range(0, 1)] private float groundNormalThreshold;
    public List<Transform> _grounds;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float fallMultiplier;
    [HideInInspector] public bool canFallJump;
    private float _startGravity;
    [SerializeField] private float dashJumpDistance = 1f;

    private bool _canCoyoteJump = true;
    [SerializeField] private float coyoteJumpTime;
    private float _cjTimer = 0;

    [SerializeField] private Animator _anim;
    [SerializeField] private FormChanger formChanger;

    private PlayerControls _playerControls;

    public PlayerStateMachine playerStateMachine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Initialize();
    }

    private void Start()
    {
        //Initialize();
    }

    private void Initialize()
    {
        _playerControls = new PlayerControls();
        _playerControls.Movement.Enable();
        //_playerControls.Movement.Jump.performed += ctx => Jump();

        canControl = true;

        _grounds = new List<Transform>();
        _startGravity = rb.gravityScale;
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
    }

    public void StopAnimation()
    {
        _anim.enabled = false;
    }

    public void StartMovement()
    {
        rb.gravityScale = _startGravity;
    }

    public void StartAnimation()
    {
        _anim.enabled = true;
    }

    public void UpdateLogic()
    {
        if (canControl)
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
        }

        if (_playerControls.Movement.Jump.ReadValue<float>() > 0)
        {
            Jump();
        } else
        {
            if (_grounded)
            {
                _jumpInput = false;
            }
        }

        if (!_grounded)
        {
            if (rb.velocity.y < 0f)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
                _anim.SetBool("isFalling", true);
            }
            else if (_playerControls.Movement.Jump.ReadValue<float>() < 1)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
            }

            if (_canCoyoteJump)
            {
                if (_cjTimer > 0)
                {
                    _cjTimer -= Time.deltaTime;
                }
                else
                {
                    _canCoyoteJump = false;
                }
            }
        }
    }

    private void Jump()
    {
        if (!_canJump || _jumpInput) { return; }

        _jumpRegistered = true;
    }

    public void UpdatePhysics()
    {
        if (canControl)
        {
            _direction.x *= speed;
            _direction.y = rb.velocity.y;
        }

        if (_jumpRegistered)
        {
            if (!formChanger.dashing && !formChanger._canCoyoteDashJump)
            {
                if (_grounded || canFallJump || _canCoyoteJump)
                {
                    formChanger.dashing = false;
                    canControl = true;
                    rb.gravityScale = _startGravity;

                    _jumpRegistered = false;
                    _jumpInput = true;
                    _direction.y = Mathf.Sqrt(-2f * Physics2D.gravity.y * rb.gravityScale * (jumpHeight + 0.25f));
                    _canJump = false;
                    _anim.SetTrigger("Jump");
                    _anim.SetBool("isFalling", false);
                }
            } else if (formChanger.dashing || formChanger._canCoyoteDashJump)
            {
                formChanger.StopDashing();
                canControl = false;
                rb.gravityScale = _startGravity;

                _jumpRegistered = false;
                _jumpInput = true;
                _direction.y = Mathf.Sqrt(-2f * Physics2D.gravity.y * rb.gravityScale * (jumpHeight + 0.25f));
                _direction.x = -transform.right.x * dashJumpDistance;
                rb.velocity = _direction;
                _canJump = false;
                _anim.SetTrigger("Jump");
                _anim.SetBool("isFalling", false);
            }
        }

        if (canControl)
        {
            rb.velocity = _direction;
        }
    }

    private void CleanGrounds()
    {
        for (int i = 0; i < _grounds.Count; i++)
        {
            if (_grounds[i] == null)
            {
                _grounds.Remove(_grounds[i]);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!formChanger.dashing && collision.GetContact(0).normal.y > -groundNormalThreshold)
        {
            canControl = true;
            formChanger._canCoyoteDashJump = false;
        }

        if (!collision.gameObject.CompareTag("Breakable Platform"))
        {
            if (formChanger.dashing && collision.GetContact(0).normal.y < groundNormalThreshold)
            {
                formChanger.StopDashing();
            }
        }

        CleanGrounds();

        if (collision.otherCollider.gameObject.CompareTag("Player"))
        {
            int g = 0;

            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector2 normal = collision.contacts[i].normal;
                if (normal.y > groundNormalThreshold)
                {
                    g++;
                    _canJump = true;
                    _canCoyoteJump = true;
                    _grounded = true;
                    _anim.SetBool("isGrounded", true);
                    if (!_grounds.Contains(collision.transform))
                    {
                        _grounds.Add(collision.transform);
                    }
                    _anim.SetBool("isFalling", false);
                }
            }

            if (g <= 0)
            {
                _grounded = false;
                _anim.SetBool("isGrounded", false);
            }
        }

        CleanGrounds();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CleanGrounds();

        if (collision.otherCollider.gameObject.CompareTag("Player"))
        {
            if (_grounds.Contains(collision.transform))
            {
                if (_grounds[0].gameObject.CompareTag("Breakable Platform"))
                {
                    _canCoyoteJump = false;
                }
                _grounds.Remove(collision.transform);

                if (_grounds.Count == 0)
                {
                    if (!_jumpRegistered)
                    {
                        _cjTimer = coyoteJumpTime;
                    }
                    _grounded = false;
                    _anim.SetBool("isGrounded", false);
                }
            }
        }

        CleanGrounds();
    }
}
