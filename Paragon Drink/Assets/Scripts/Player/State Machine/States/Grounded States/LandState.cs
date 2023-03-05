using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LandState : GroundedState
{
    private float _timer = 0.133f;

    public LandState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _animator.CrossFade(_playerController.land, 0f);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _currentSuperState.ChangeSubState(new IdleState(_playerStateMachine, _playerController, _animator));
        }
    }
}
