using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rb2D;

    public float speed;
    Vector2 move;

    public float jumpHeight;
    bool jumpRegistered = false;
    bool grounded = true;
    Transform currentGround;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    [Range(0f, 1f)]
    public float groundNormalThreshold = 0.6f;

    Animator playerAnimator;

    bool dead;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //playerAnimator = GetComponent<Animator>();

        dead = false;
    }

    private void Update()
    {
        if (!dead)
        {
            if (grounded && Input.GetButtonDown("Jump"))
            {
                jumpRegistered = true;
            }
        }

        if (!grounded)
        {
            if (rb2D.velocity.y < 0f)
                rb2D.velocity += Vector2.up *  Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            else if (!Input.GetButton("Jump"))
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }

        //playerAnimator.SetBool("Grounded", grounded);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, rb2D.velocity, Color.green);

        if (!dead)
            move = Vector2.right * Input.GetAxis("Horizontal") * speed;
        move.y = rb2D.velocity.y;

        if (jumpRegistered)
        {
            jumpRegistered = false;
            move.y = Mathf.Sqrt(-2f * Physics2D.gravity.y * rb2D.gravityScale * (jumpHeight + 0.5f));
            //playerAnimator.SetTrigger("Jump");
        }

        rb2D.velocity = move;
    }

    /*public void Death()
    {
        dead = true;
        Camera.main.GetComponent<SmoothCamera>().gameOver = true;
        GetComponent<BoxCollider2D>().enabled = false;
        rb2D.constraints = RigidbodyConstraints2D.None;
        rb2D.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        rb2D.AddTorque(45f);
        move.x = 0;

        StartCoroutine(DeathTimer());
    }*/

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;
        if (normal.y > groundNormalThreshold)
        {
            //if (!collision.gameObject.CompareTag("Enemy"))
            //{
                grounded = true;
                currentGround = collision.transform;
            //} else
            //{
                //jumpRegistered = true;
                //collision.gameObject.GetComponent<EnemyController>().Death();
            //}
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
