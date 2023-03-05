using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionState : State
{
    private PlayerMovement _playerMovement;
    private PlayerController _playerController;

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _playerMovement = PlayerMovement.Instance;
        _playerController = _stateMachine.playerController;

        _playerController.Idle();

        //_playerStateMachine.ChangeState(new StillState());
    }
}