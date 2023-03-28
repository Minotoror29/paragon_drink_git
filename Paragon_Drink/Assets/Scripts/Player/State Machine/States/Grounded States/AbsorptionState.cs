using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorptionState : GroundedState
{
    private float _timer = 0.455f;

    public AbsorptionState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
        _soundPath = "event:/Player/juan_absorption";
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _playerController.currentWater.GetComponent<Animator>().CrossFade("Water_Empty", 0);
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
