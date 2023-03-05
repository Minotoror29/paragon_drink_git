using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : GroundedState
{
    public RunState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {

    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _animator.CrossFade(_playerController.run, 0f);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_direction.magnitude < 0.1f)
        {
            _currentSuperState.ChangeSubState(new IdleState(_playerStateMachine, _playerController, _animator));
        }
    }
}
