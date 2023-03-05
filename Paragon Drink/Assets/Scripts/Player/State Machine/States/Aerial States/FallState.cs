using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : AerialState
{
    private bool _canFallJump;

    public FallState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator, bool canFallJump) : base(playerStateMachine, playerController, animator)
    {
        _canFallJump = canFallJump;
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _animator.CrossFade(_playerController.fall, 0f);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_canFallJump && _jumpInput)
        {
            _currentSuperState.ChangeSubState(new JumpState(_playerStateMachine, _playerController, _animator));
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (_jumpInput)
        {
            _playerController.requireNewJumpPress = true;
        }
    }
}
