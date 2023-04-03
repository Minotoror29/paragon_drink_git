using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : Manager
{
    [SerializeField] private Transform screensParent;
    private List<CutsceneScreen> _screens;

    private CutsceneScreen _currentScreen;

    private MenuControls _menuControls;
    private bool _canSwitch = false;

    public override void Initialize(GameManager gameManager, StateMachine stateMachine)
    {
        base.Initialize(gameManager, stateMachine);

        _screens = new List<CutsceneScreen>();
        for (int i = 0; i < screensParent.childCount; i++)
        {
            _screens.Add(screensParent.GetChild(i).GetComponent<CutsceneScreen>());
            _screens[i].Initialize(this, i);
        }

        _menuControls = new MenuControls();
        _menuControls.Cutscene.Enable();
        _menuControls.Cutscene.Skip.performed += ctx => SkipScreen();

        StartCutscene();
    }

    private void StartCutscene()
    {
        _currentScreen = _screens[0];

        _currentScreen.EnterScreen();
    }

    private void SkipScreen()
    {
        if (!_canSwitch) return;

        _canSwitch = false;
        _currentScreen.ExitScreen();
    }

    public void SwitchScreen()
    {
        if (_screens.IndexOf(_currentScreen) + 1 < _screens.Count)
        {
            _currentScreen = _screens[_screens.IndexOf(_currentScreen) + 1];
            _currentScreen.EnterScreen();
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void DisplaySkipButton()
    {
        _canSwitch = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!_canSwitch)
        {
            _currentScreen.UpdateLogic();
        }
    }
}
