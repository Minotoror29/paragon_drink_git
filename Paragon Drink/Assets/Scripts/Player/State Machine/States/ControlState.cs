using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlState : PlayerState
{
    public override void Enter(State previousState)
    {
        base.Enter(previousState);

        playerMovement = PlayerMovement.Instance;

        playerMovement.StartMovement();
        playerMovement.StartAnimation();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        playerMovement.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        playerMovement.UpdatePhysics();
    }
}
