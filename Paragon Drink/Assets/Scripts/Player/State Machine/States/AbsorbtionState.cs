using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbtionState : PlayerState
{
    public override void Enter(State previousState)
    {
        base.Enter(previousState);

        playerMovement.StopMovement();
    }
}
