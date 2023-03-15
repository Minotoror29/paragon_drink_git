using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : State
{
    private PlayerController _playerController;

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _playerController = _stateMachine.playerController;

        _playerController.animator.enabled = true;
        _playerController._playerControls.Enable();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _playerController.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        _playerController.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();

        _playerController.Idle();

        _playerController.animator.enabled = false;
        _playerController._playerControls.Disable();
    }
}
