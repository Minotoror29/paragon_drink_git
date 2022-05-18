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

    [SerializeField] private GameObject bubblePrefab;

    private void Start()
    {
        controller = GetComponent<PlayerMovement>();

        controller.jumpHeight = dehydratedJumpHeight;
        controller.canCoyoteJump = true;
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
            controller.canCoyoteJump = false;
        } else
        {
            transform.localScale /= 2;
            controller.jumpHeight = dehydratedJumpHeight;
            controller.canCoyoteJump = true;
        }
    }

    private void ShootBubble()
    {
        Instantiate(bubblePrefab, transform.position, Quaternion.identity);
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
