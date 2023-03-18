using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{
    private MenuManager _menuManager;
    private MenuControls _menuControls;

    public MenuState()
    {
        _menuManager = MenuManager.Instance;
        _menuControls = _menuManager.menuControls;
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _menuControls.Enable();
    }

    public override void Exit()
    {
        base.Exit();

        _menuControls.Disable();
    }
}
