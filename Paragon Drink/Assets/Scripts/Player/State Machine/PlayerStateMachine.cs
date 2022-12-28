using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public State _currentState;

    public void ChangeState(State newState)
    {
        _currentState?.Exit();
        State previousState = _currentState;
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
