using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine _stateMachine;
    protected State _previousState;

    protected State _currentSuperState;
    protected State _currentSubState;

    public State()
    {
        _stateMachine = StateMachine.Instance;
    }

    public void ChangeSubState(PlayerState newSubstate)
    {
        _currentSubState?.Exit();
        State previousState = _currentSubState;
        _currentSubState = newSubstate;
        _currentSubState.Enter(previousState, this);
    }

    public virtual void Enter(State previousState, State superState)
    {
        _previousState = previousState;
        _currentSuperState = superState;
    }

    public virtual void UpdateLogic()
    {

    }

    public virtual void UpdatePhysics()
    {

    }

    public virtual void EnterCollision(Collision2D collision)
    {
        _currentSubState?.EnterCollision(collision);
    }

    public virtual void ExitCollision(Collision2D collision)
    {
        _currentSubState?.ExitCollision(collision);
    }

    public virtual void Exit()
    {

    }
}
