using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorptionState : GroundedState
{
    private float _timer = 0.455f;

    public AbsorptionState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _animator.CrossFade(_playerController.absorption, 0f);
        _playerController.Idle();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _playerStateMachine.ChangeState(new HydratedState(_playerStateMachine, _playerController, _animator, new IdleState(_playerStateMachine, _playerController, _animator)));
        }
    }

    public override void UpdatePhysics()
    {
        
    }
}
