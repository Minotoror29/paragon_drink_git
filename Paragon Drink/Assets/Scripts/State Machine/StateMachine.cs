using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private static StateMachine instance;
    public static StateMachine Instance => instance;

    public State _currentState;

    public PlayerController playerController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Initialize()
    {
        playerController.Initialize();

        ChangeState(new PlayState());
    }

    public void ChangeState(State newState)
    {
        _currentState?.Exit();
        State previousState = _currentState;
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
}
