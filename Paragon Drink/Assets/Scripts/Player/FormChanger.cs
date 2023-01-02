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
    [SerializeField] private float dashingTime;
    private float _dashTimer;
    private float _originalGravityScale;

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
        if (dashing)
        {
            if (_dashTimer > 0)
            {
                _dashTimer -= Time.deltaTime;
                
            } else
            {
                dashing = false;
                controller.rb.gravityScale = _originalGravityScale;
                _canCoyoteDashJump = true;
                _coyoteDashJumpTimer = coyoteDashJumpTime;
            }
        }

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
            if (_dashTimer > 0)
            {
                controller.rb.velocity = -transform.right * dashingSpeed;
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

            //transform.localScale /= 2;
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

        //transform.localScale *= 2;
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
        animator.SetTrigger("Dash");

        controller.canControl = false;
        controller.rb.gravityScale = 0;

        dashing = true;
        _dashTimer = dashingTime;
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
