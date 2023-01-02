using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillState : PlayerState
{
    public override void Enter(State previousState)
    {
        base.Enter(previousState);

        playerMovement = PlayerMovement.Instance;

        playerMovement.StopMovement();
        playerMovement.StopAnimation();
    }
}
