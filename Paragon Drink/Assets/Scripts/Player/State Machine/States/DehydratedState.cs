using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DehydratedState : PlayerState
{
    private PlayerState _initialSubState;

    public DehydratedState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator, PlayerState subState) : base(playerStateMachine, playerController, animator)
    {
        _playerController.idle = "Juan-DeHydrated-Idle";
        _playerController.run = "Juan-DeHydrated-RunStart";
        _playerController.jump = "Juan-DeHydrated-Jump";
        _playerController.fall = "Juan-DeHydrated-Fall";
        _playerController.land = "Juan-DeHydrated-Land";

        _initialSubState = subState;
}

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        ChangeSubState(_initialSubState);

        _movementSpeed = _playerController.dehydratedMovementSpeed;
        _jumpHeight = _playerController.dehydratedJumpHeight;

        _playerController.feet.SetActive(false);
    }

    public override void Action()
    {
        base.Action();

        if (_playerController.inWater && _playerController.currentGround != null)
        {
            _playerStateMachine.ChangeState(new AbsorptionState(_playerStateMachine, _playerController, _animator));
        }
    }
}
