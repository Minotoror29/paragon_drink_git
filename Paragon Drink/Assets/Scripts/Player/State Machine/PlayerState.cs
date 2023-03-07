using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public abstract class PlayerState : State
{
    protected PlayerStateMachine _playerStateMachine;
    protected PlayerController _playerController;
    protected Animator _animator;
    protected PlayerControls _controls;

    protected Vector2 _direction;
    protected bool _jumpInput;
    protected bool _jumpRegistered = false;

    public float _movementSpeed;
    public float _jumpHeight;

    public PlayerState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator)
    {
        _playerStateMachine = playerStateMachine;
        _playerController = playerController;
        _animator = animator;
        _controls = _playerController._playerControls;
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        if (_currentSuperState != null)
        {
            PlayerState sState = (PlayerState)_currentSuperState;
            _movementSpeed = sState._movementSpeed;
            _jumpHeight = sState._jumpHeight;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        GetInputs();
        DetectGround();
        JumpHeight();

        _currentSubState?.UpdateLogic();
    }

    public void GetInputs()
    {
        _direction = _controls.Movement.Movement.ReadValue<Vector2>();

        if (_controls.Movement.Jump.ReadValue<float>() > 0.1f)
        {
            _jumpInput = true;
        }
        else
        {
            _jumpInput = false;
            _playerController.requireNewJumpPress = false;
        }
    }

    private void DetectGround()
    {
        RaycastHit2D ray = Physics2D.BoxCast(
            (Vector2)_playerController.transform.position + (Vector2.up * (0.9f * (_playerController.transform.localScale.x - 0.075f) / 2)),
            _playerController.transform.localScale * 0.9f,
            0f,
            Vector2.down,
            0f,
            _playerController.groundLayer);

        if (ray)
        {
            if (ray.normal.y > 0.6f)
            {
                _playerController.currentGround = ray.transform;
            } else
            {
                _playerController.currentGround = null;
            }
        } else
        {
            _playerController.currentGround = null;
        }
    }

    private void JumpHeight()
    {
        RaycastHit2D ray = Physics2D.BoxCast(
            (Vector2)_playerController.transform.position + (Vector2.up * (0.9f * (_playerController.transform.localScale.x - 0.075f) / 2)),
            _playerController.transform.localScale * 0.9f,
            0f,
            Vector2.down,
            0.25f,
            _playerController.groundLayer);

        if (ray)
        {
            if (ray.normal.y > 0.6f)
            {
                _playerController.canJump = true;
            } else
            {
                _playerController.canJump = false;
            }
        }
        else
        {
            _playerController.canJump = false;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        _currentSubState?.UpdatePhysics();
    }    

    public virtual void Action()
    {
        
    }

    public override void Exit()
    {
        base.Exit();

        GetInputs();

        if (_jumpInput)
        {
            _playerController.requireNewJumpPress = true;
        }
    }
}
