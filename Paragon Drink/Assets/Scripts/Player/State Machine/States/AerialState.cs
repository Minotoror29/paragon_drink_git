using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AerialState : PlayerState
{
    private bool _canDetectGround;
    private float _securityTimer = 0.1f;

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

        if (_playerController.currentGround != null && _canDetectGround)
        {
            _currentSuperState.ChangeSubState(new LandState(_playerStateMachine, _playerController, _animator));
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
