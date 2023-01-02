using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    public PlayerMovement playerMovement;

    public override void Enter(State previousState)
    {
        base.Enter(previousState);

        playerMovement = PlayerMovement.Instance;
    }
}
