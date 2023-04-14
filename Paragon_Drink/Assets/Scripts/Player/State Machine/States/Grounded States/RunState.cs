using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class RunState : GroundedState
{
    public RunState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
        //_soundPath = "event:/Player/juan_dehydrated_footsteps";
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _animator.CrossFade(_playerController.run, 0f);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_direction.magnitude < 0.01f)
        {
            _currentSuperState.ChangeSubState(new IdleState(_playerStateMachine, _playerController, _animator));
        }
    }

    public override void Exit()
    {
        base.Exit();

        //_soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
