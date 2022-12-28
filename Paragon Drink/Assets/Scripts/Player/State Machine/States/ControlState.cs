using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlState : State
{
    private PlayerMovement _playerMovement;

    public override void Enter(State previousState)
    {
        base.Enter(previousState);

        _playerMovement = PlayerMovement.Instance;

        _playerMovement.StartMovement();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _playerMovement.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        _playerMovement.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();

        _playerMovement.StopMovement();
    }
}
