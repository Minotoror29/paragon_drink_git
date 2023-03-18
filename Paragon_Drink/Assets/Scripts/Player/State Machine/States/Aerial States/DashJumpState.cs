using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashJumpState : AerialState
{
    private float _timer = 0.1f;

    public DashJumpState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _animator.CrossFade(_playerController.jump, 0f);

        _playerController.Jump(_jumpHeight);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            if (_playerController.rb.velocity.y < 0f)
            {
                _currentSuperState.ChangeSubState(new FallState(_playerStateMachine, _playerController, _animator, false));
            }
        }
    }

    public override void UpdatePhysics()
    {
        _playerController.Fall();
    }

    public override void EnterCollision(Collision2D collision)
    {
        base.EnterCollision(collision);

        if (collision.GetContact(0).normal.y == -1)
        {
            _currentSuperState.ChangeSubState(new LowFallState(_playerStateMachine, _playerController, _animator));
        }
    }
}
