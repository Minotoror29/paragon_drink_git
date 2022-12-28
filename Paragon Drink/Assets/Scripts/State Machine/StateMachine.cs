using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private static StateMachine instance;
    public static StateMachine Instance => instance;

    public State _currentState;

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

    private void Start()
    {
        ChangeState(new PlayState());
    }

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
