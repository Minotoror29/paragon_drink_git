using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState _currentState;

    public void ChangeState(PlayerState newState)
    {
        _currentState?.Exit();
        PlayerState previousState = _currentState;
        _currentState = newState;
        _currentState.Enter(previousState);
    }

    private void Update()
    {
        _currentState.UpdateLogic();
    }

    private void FixedUpdate()
    {
        _currentState.UpdatePhysics();
    }
}
