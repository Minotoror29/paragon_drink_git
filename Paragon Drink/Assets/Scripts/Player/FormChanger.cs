using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Form { Dehydrated, Hydrated }

public class FormChanger : MonoBehaviour
{
    private bool inWater = false;
    [HideInInspector] public Form form = Form.Dehydrated;

    private PlayerMovement controller;

    [SerializeField] private float dehydratedJumpHeight;
    [SerializeField] private float hydratedJumpHeight;

    [SerializeField] private float dehydratedSpeed;
    [SerializeField] private float hydratedSpeed;

    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Transform shootPoint;

    [HideInInspector] public bool dashing = false;
    [SerializeField] private float dashingSpeed;
    [SerializeField] private float dashingDistance;
    private Vector2 dashTarget;
    private float _originalGravityScale;
    [SerializeField] private LayerMask groundLayer;

    [HideInInspector] public bool _canCoyoteDashJump = false;
    [SerializeField] private float coyoteDashJumpTime;
    [HideInInspector] public float _coyoteDashJumpTimer;

    private PlayerControls _playerControls;

    [SerializeField] private Animator animator;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _playerControls = new PlayerControls();
        _playerControls.Movement.Enable();
        _playerControls.Movement.Action.performed += ctx => Action();

        controller = GetComponent<PlayerMovement>();
        _originalGravityScale = controller.rb.gravityScale;

        controller.jumpHeight = dehydratedJumpHeight;
        controller.speed = dehydratedSpeed;
        controller.canFallJump = true;
    }

    private void Update()
    {
        

        if (_canCoyoteDashJump)
        {
            if (_coyoteDashJumpTimer > 0)
            {
                _coyoteDashJumpTimer -= Time.deltaTime;
            } else
            {
                _canCoyoteDashJump = false;
                controller.canControl = true;
            }
        }
    }

    private void Action()
    {
        if (inWater && form == Form.Dehydrated)
        {
            ChangeForm(Form.Hydrated);
        }
        else if (form == Form.Hydrated)
        {
            ChangeForm(Form.Dehydrated);
            ShootBubble();
            Dash();
        }
    }

    private void FixedUpdate()
    {
        if (dashing)
        {
            if ((Vector2)transform.position != dashTarget)
            {
                transform.position = Vector2.MoveTowards(transform.position, dashTarget, dashingSpeed);
                controller.rb.velocity = Vector2.zero;
            }
            else
            {
                StopDashing();
                controller.rb.velocity = -transform.right * 10;
            }
        }
    }

    private void ChangeForm(Form form)
    {
        this.form = form;

        if (this.form == Form.Hydrated)
        {
            StartCoroutine(Absorbing());
        } else
        {
            animator.SetBool("Hydrated", false);

            controller.jumpHeight = dehydratedJumpHeight;
            controller.speed = dehydratedSpeed;
            controller.canFallJump = true;
        }
    }

    private IEnumerator Absorbing()
    {
        animator.SetTrigger("Absorb");
        animator.SetBool("Hydrated", true);
        animator.SetBool("Absorbing", true);

        controller.playerStateMachine.ChangeState(new AbsorbtionState());

        yield return new WaitForSeconds(0.5f);

        controller.playerStateMachine.ChangeState(new ControlState());

        animator.SetBool("Absorbing", false);

        controller.jumpHeight = hydratedJumpHeight;
        controller.speed = hydratedSpeed;
        controller.canFallJump = false;
    }

    private void ShootBubble()
    {
        Instantiate(bubblePrefab, shootPoint.position, transform.rotation);
    }
    private void Dash()
    {
        animator.SetBool("Dashing", true);

        controller.canControl = false;
        controller.rb.gravityScale = 0;

        dashing = true;

        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position,
            new Vector2(-Mathf.Sign(transform.rotation.y), 0),
            dashingDistance,
            groundLayer);

        if (hit.collider != null)
        {
            dashTarget = new Vector2(hit.point.x - ((transform.localScale.x / 4) * -Mathf.Sign(transform.rotation.y)), hit.point.y);
        } else
        {
            dashTarget = new Vector2(transform.position.x - dashingDistance * Mathf.Sign(transform.rotation.y), transform.position.y);
        }
    }

    public void StopDashing()
    {
        dashing = false;
        controller.rb.gravityScale = _originalGravityScale;
        _canCoyoteDashJump = true;
        _coyoteDashJumpTimer = coyoteDashJumpTime;

        animator.SetBool("Dashing", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            inWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            inWater = false;
        }
    }
}
