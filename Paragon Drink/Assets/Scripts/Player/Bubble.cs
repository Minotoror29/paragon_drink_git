using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;

    private Vector2 _direction;
    [SerializeField] private float speed;
    private bool _moving = true;

    public void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_moving)
        {
            _direction = Vector2.right * speed;
        } else
        {
            _direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.right * _direction;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    _moving = false;
    //    //_rb.velocity = Vector2.zero;
    //    _animator.CrossFade("Splash", 0f);
    //    Destroy(gameObject, 0.545f);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _moving = false;
        //_rb.velocity = Vector2.zero;
        _animator.CrossFade("Splash", 0f);
        Destroy(gameObject, 0.545f);
    }
}
