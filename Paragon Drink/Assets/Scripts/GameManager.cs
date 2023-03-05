using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;

    private void Start()
    {
        stateMachine.Initialize();
    }

    private void Update()
    {
        stateMachine.UpdateLogic();
    }

    private void FixedUpdate()
    {
        stateMachine.UpdatePhysics();
    }
}
