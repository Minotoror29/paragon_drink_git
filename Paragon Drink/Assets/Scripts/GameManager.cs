using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private LevelsManager levelsManager;

    public int itemsCollected = 0;

    [SerializeField] private Credits credits;

    private void Start()
    {
        stateMachine.Initialize();
        levelsManager.Initialize(this);
        credits.Initialize(this);
    }

    private void Update()
    {
        stateMachine.UpdateLogic();
    }

    private void FixedUpdate()
    {
        stateMachine.UpdatePhysics();
    }

    public void CollectItem()
    {
        itemsCollected++;
    }

    public void StartCredits()
    {
        stateMachine.ChangeState(new TransitionState());
        credits.StartCredits();
    }
}
