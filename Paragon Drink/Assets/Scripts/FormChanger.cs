using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool dashing = false;
    [SerializeField] private float dashingSpeed;
    [SerializeField] private float dashingTime;
    private float t;
    private float originalGravityScale;

    private void Start()
    {
        controller = GetComponent<PlayerMovement>();
        originalGravityScale = controller.rb.gravityScale;

        controller.jumpHeight = dehydratedJumpHeight;
        controller.speed = dehydratedSpeed;
        controller.canFallJump = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            if (inWater && form == Form.Dehydrated)
            {
                ChangeForm(Form.Hydrated);
            } else if (form == Form.Hydrated)
            {
                ChangeForm(Form.Dehydrated);
                ShootBubble();
                Dash();
            }
        }

        if (dashing)
        {
            if (t > 0)
            {
                t -= Time.deltaTime;
                
            } else
            {
                dashing = false;
                controller.canControl = true;
                controller.rb.gravityScale = originalGravityScale;
            }
        }
    }

    private void FixedUpdate()
    {
        if (dashing)
        {
            if (t > 0)
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
            transform.localScale *= 2;
            controller.jumpHeight = hydratedJumpHeight;
            controller.speed = hydratedSpeed;
            controller.canFallJump = false;
        } else
        {
            transform.localScale /= 2;
            controller.jumpHeight = dehydratedJumpHeight;
            controller.speed = dehydratedSpeed;
            controller.canFallJump = true;
        }
    }

    private void ShootBubble()
    {
        Instantiate(bubblePrefab, shootPoint.position, transform.rotation);
    }
    private void Dash()
    {
        controller.canControl = false;
        controller.rb.gravityScale = 0;

        dashing = true;
        t = dashingTime;
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
