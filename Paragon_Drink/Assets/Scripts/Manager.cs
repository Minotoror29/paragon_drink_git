using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    protected GameManager _gameManager;
    protected StateMachine _stateMachine;

    public virtual void Initialize(GameManager gameManager, StateMachine stateMachine)
    {
        _gameManager = gameManager;
        _stateMachine = stateMachine;
    }

    public virtual void UpdateLogic()
    {

    }
}
