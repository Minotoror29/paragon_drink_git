using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FMOD.Studio;
using FMODUnity;

public class MenuManager : Manager
{
    private static MenuManager instance;
    public static MenuManager Instance => instance;

    public GameParameters _gameParameters;

    [HideInInspector] public MenuControls menuControls;
    public PlayerInput _menuInput;

    private ControlSchemeImage[] _controlSchemeImages;

    [SerializeField] private GameObject playButton;

    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject frenchButton;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private GameObject fullscreen;
    [SerializeField] private GameObject windowed;

    private WindowMode currentWindowMode = WindowMode.Fullscreen;

    [SerializeField] private List<GameObject> cracks;
    private int _crack = 0;
    [SerializeField] private JuanButton juanButton;
    private List<EventInstance> _crackSounds;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        } else
        {
            instance = this;
        }
    }

    public override void Initialize(GameManager gameManager, StateMachine stateMachine)
    {
        base.Initialize(gameManager, stateMachine);

        _gameParameters = _gameManager.gameParameters;

        menuControls = new MenuControls();
        menuControls.Menu.QuitMenu.performed += ctx => HideOptionsMenu();

        //_menuInput = GetComponent<PlayerInput>();
        _menuInput = _gameManager.GetComponent<PlayerInput>();
        _controlSchemeImages = FindObjectsOfType<ControlSchemeImage>(true);
        foreach (ControlSchemeImage image in _controlSchemeImages)
        {
            image.Initialize(menuControls, _menuInput);
        }

        _crackSounds = new List<EventInstance>();
        _crackSounds.Add(RuntimeManager.CreateInstance("event:/Menu/glass_break_1"));
        _crackSounds.Add(RuntimeManager.CreateInstance("event:/Menu/glass_break_2"));
        _crackSounds.Add(RuntimeManager.CreateInstance("event:/Menu/glass_break_3"));
        _crackSounds.Add(RuntimeManager.CreateInstance("event:/Menu/glass_break_4"));
    }

    public void SwitchToMenuState()
    {
        _stateMachine.ChangeState(new MenuState());
    }

    public void SwitchToPlayState()
    {
        _stateMachine.ChangeState(new PlayState());
    }

    public void Play()
    {
        if (_crack == 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        if (_crack > 0)
        {
            cracks[_crack - 1].SetActive(false);
        }
        cracks[_crack].SetActive(true);
        _crack++;

        juanButton.Crack();

        CameraManager.Instance.ShakeCam(5f, 0.1f);

        _crackSounds[_crack - 1].start();
    }

    public void DisplayOptionsMenu()
    {
        optionsMenu.SetActive(true);
        menuControls.Menu.Enable();
        EventSystem.current.SetSelectedGameObject(frenchButton);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SwitchToMenuState();
        }
    }
    
    public void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
        menuControls.Menu.Disable();
        EventSystem.current.SetSelectedGameObject(playButton);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SwitchToPlayState();
        }
    }

    public void ChangeLanguage(string language)
    {
        Language l = (Language)System.Enum.Parse(typeof(Language), language);
        _gameParameters.ChangeLanguage(l);
    }

    public void ChangeSFXVolume()
    {
        _gameParameters.ChangeSFXVolume(sfxSlider.value);
    }

    public void ChangeMusicVolume()
    {
        _gameParameters.ChangeMusicVolume(musicSlider.value);
    }

    public void ChangeWindowMode()
    {
        switch (currentWindowMode)
        {
            case WindowMode.Fullscreen:
                currentWindowMode = WindowMode.Windowed;
                fullscreen.SetActive(false);
                windowed.SetActive(true);
                break;
            case WindowMode.Windowed:
                currentWindowMode = WindowMode.Fullscreen;
                windowed.SetActive(false);
                fullscreen.SetActive(true);
                break;
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }
}

public enum Language { French, English }

public enum WindowMode { Fullscreen, Windowed }
