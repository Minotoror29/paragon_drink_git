using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundedState : PlayerState
{
    public GroundedState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _playerController.Grounded();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_currentSuperState != null)
        {
            if (_jumpInput && !_playerController.requireNewJumpPress)
            {
                _currentSuperState.ChangeSubState(new JumpState(_playerStateMachine, _playerController, _animator));
            }
            else if (_playerController.currentGround == null)
            {
                _currentSuperState.ChangeSubState(new FallState(_playerStateMachine, _playerController, _animator, true));
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        _playerController.Move(_direction, _movementSpeed);
    }

    //public override void ExitCollision(Collision2D collision)
    //{
    //    base.ExitCollision(collision);

    //    if (_playerController.currentGround == collision.transform)
    //    {
    //        _playerController.currentGround = null;
    //        _playerStateMachine.ChangeState(new FallState(_playerStateMachine, _playerController, _animator));
    //    }
    //}
}
