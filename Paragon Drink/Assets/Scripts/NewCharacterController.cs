using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterController : MonoBehaviour
{
    private PlayerControls _playerControls;

    private Rigidbody2D _rb;

    private Vector2 _direction;
    [SerializeField] private float speed = 1;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _playerControls = new PlayerControls();
        _playerControls.Movement.Enable();

        _rb = GetComponent<Rigidbody2D>();
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

        _direction.x *= speed;
        _direction.y = _rb.velocity.y;

        _rb.velocity = _direction;
    }
}
