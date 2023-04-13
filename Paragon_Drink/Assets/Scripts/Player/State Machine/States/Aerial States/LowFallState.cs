using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowFallState : AerialState
{
    public LowFallState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _animator.CrossFade(_playerController.fall, 0f);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_playerController.rb.velocity.y < 0f)
        {
            _currentSuperState.ChangeSubState(new FallState(_playerStateMachine, _playerController, _animator, false));
        }

        _playerController.LowFall();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        //_playerController.LowFall();
    }
}
