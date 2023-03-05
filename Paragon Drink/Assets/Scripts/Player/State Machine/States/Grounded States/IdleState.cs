using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{
    public IdleState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
        
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _animator.CrossFade(_playerController.idle, 0f);

        _playerController.Idle();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_direction.magnitude > 0.1f)
        {
            _currentSuperState.ChangeSubState(new RunState(_playerStateMachine, _playerController, _animator));
        }
    }
}
