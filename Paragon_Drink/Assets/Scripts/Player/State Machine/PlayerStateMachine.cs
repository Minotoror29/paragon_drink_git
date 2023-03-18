using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState _currentState;

    public void Initialize(PlayerState startingState)
    {
        ChangeState(startingState);
    }

    public void ChangeState(PlayerState newState)
    {
        _currentState?.Exit();
        PlayerState previousState = _currentState;
        _currentState = newState;
        _currentState.Enter(previousState, null);
    }

    public void UpdateLogic()
    {
        _currentState.UpdateLogic();
    }

    public void UpdatePhysics()
    {
        _currentState.UpdatePhysics();
    }

    public void EnterCollision(Collision2D collision)
    {
        _currentState.EnterCollision(collision);
    }

    public void ExitCollision(Collision2D collision)
    {
        _currentState.ExitCollision(collision);
    }

    public void Action()
    {
        _currentState.Action();
    }
}
