using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public StateMachine _stateMachine;
    public State _previousState;

    public State()
    {
        _stateMachine = StateMachine.Instance;
    }

    public virtual void Enter(State previousState)
    {
        _previousState = previousState;
    }

    public virtual void UpdateLogic()
    {

    }

    public virtual void UpdatePhysics()
    {

    }

    public virtual void Exit()
    {

    }
}
