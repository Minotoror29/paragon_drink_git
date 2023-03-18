using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AerialState : PlayerState
{
    private bool _canDetectGround;
    private float _securityTimer = 0.1f;

    private float _anticipatedJumpTimer = 0.1f;
    private bool _canAnticipateJump = true;

    public AerialState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {

    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _canDetectGround = false;
        _jumpRegistered = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_securityTimer > 0)
        {
            _securityTimer -= Time.deltaTime;
        } else
        {
            _canDetectGround = true;
        }

        if (_jumpInput)
        {
            if (_anticipatedJumpTimer > 0)
            {
                _anticipatedJumpTimer -= Time.deltaTime;
                _canAnticipateJump = true;
            }
            else
            {
                _canAnticipateJump = false;
            }
        } else
        {
            _anticipatedJumpTimer = 0.1f;
            _canAnticipateJump = false;
        }

        if (_playerController.currentGround != null && _canDetectGround)
        {
            if (_playerController.rb.velocity.y <= 0f || !_playerController.currentGround.gameObject.CompareTag("Breakable Platform"))
            {
                _currentSuperState.ChangeSubState(new LandState(_playerStateMachine, _playerController, _animator, _canAnticipateJump));
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        _playerController.Move(_direction, _movementSpeed);
        _playerController.Fall();
    }

    //public override void EnterCollision(Collision2D collision)
    //{
    //    base.EnterCollision(collision);

    //    if (collision.GetContact(0).normal.y > 0.6f)
    //    {
    //        _playerController.currentGround = collision.transform;
    //        //_playerController.AddGround(collision.transform);
    //        _playerStateMachine.ChangeState(new LandState(_playerStateMachine, _playerController, _animator));
    //    }
    //}
}
