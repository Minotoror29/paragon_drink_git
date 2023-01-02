using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : State
{
    private PlayerMovement _playerMovement;
    private PlayerStateMachine _playerStateMachine;

    public override void Enter(State previousState)
    {
        base.Enter(previousState);

        _playerMovement = PlayerMovement.Instance;
        _playerStateMachine = _playerMovement.playerStateMachine;

        _playerStateMachine.ChangeState(new ControlState());
    }
}
