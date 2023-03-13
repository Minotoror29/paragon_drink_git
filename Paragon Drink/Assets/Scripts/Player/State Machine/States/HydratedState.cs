using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydratedState : PlayerState
{
    private PlayerState _initialSubState;

    public HydratedState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator, PlayerState subState) : base(playerStateMachine, playerController, animator)
    {
        _playerController.idle = "Juan-Hydrated-Idle";
        _playerController.run = "Juan-Hydrated-Run";
        _playerController.jump = "Juan-Hydrated-Jump";
        _playerController.fall = "Juan-Hydrated-Fall";
        _playerController.land = "Juan-Hydrated-Land";

        _initialSubState = subState;

        _playerController.size = 1;
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        ChangeSubState(_initialSubState);

        _movementSpeed = _playerController.hydratedMovementSpeed;
        _jumpHeight = _playerController.hydratedJumpHeight;

        _playerController.feet.SetActive(true);
    }

    public override void Action()
    {
        base.Action();

        _playerStateMachine.ChangeState(new DehydratedState(_playerStateMachine, _playerController, _animator, new DashState(_playerStateMachine, _playerController, _animator)));
    }

    //public override void EnterCollision(Collision2D collision)
    //{
    //    base.EnterCollision(collision);

    //    HorizontalPlatform platform = collision.gameObject.GetComponent<HorizontalPlatform>();

    //    if (platform != null)
    //    {
    //        platform.Break();
    //    }
    //}
}
