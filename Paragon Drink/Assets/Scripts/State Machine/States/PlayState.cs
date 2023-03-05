using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : State
{
    private PlayerMovement _playerMovement;
    private PlayerController _playerController;

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _playerMovement = PlayerMovement.Instance;
        _playerController = _stateMachine.playerController;
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
}
