using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AerialState
{
    private float _timer = 0.1f;

    public JumpState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
        _soundPath = "event:/Player/juan_dehydrated_jump";
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

        if (!_jumpInput)
        {
            _currentSuperState.ChangeSubState(new LowFallState(_playerStateMachine, _playerController, _animator));
        } else if (_playerController.rb.velocity.y < 0f && _timer <= 0f)
        {
            _currentSuperState.ChangeSubState(new FallState(_playerStateMachine, _playerController, _animator, false));
        }
    }

    public override void EnterCollision(Collision2D collision)
    {
        base.EnterCollision(collision);

        if (collision.GetContact(0).normal.y == -1 && !collision.gameObject.CompareTag("Breakable Platform"))
        {
            _currentSuperState.ChangeSubState(new LowFallState(_playerStateMachine, _playerController, _animator));
        }
    }
}
