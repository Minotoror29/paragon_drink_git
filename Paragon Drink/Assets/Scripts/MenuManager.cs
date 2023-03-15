using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : Manager
{
    private static MenuManager instance;
    public static MenuManager Instance => instance;

    [HideInInspector] public MenuControls menuControls;

    [SerializeField] private GameObject playButton;

    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject frenchButton;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Image fullscreen;
    [SerializeField] private Image windowed;

    private WindowMode currentWindowMode = WindowMode.Fullscreen;

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

        DontDestroyOnLoad(gameObject);

        menuControls = new MenuControls();
        menuControls.Menu.QuitMenu.performed += ctx => HideOptionsMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DisplayOptionsMenu()
    {
        optionsMenu.SetActive(true);
        menuControls.Menu.Enable();
        EventSystem.current.SetSelectedGameObject(frenchButton);
    }
    
    public void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
        menuControls.Menu.Disable();
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void ChangeLanguage(string language)
    {
        Language l = (Language)System.Enum.Parse(typeof(Language), language);
    }

    public void ChangeSFXVolume()
    {
    }

    public void ChangeMusicVolume()
    {
    }

    public void ChangeWindowMode()
    {
        switch (currentWindowMode)
        {
            case WindowMode.Fullscreen:
                currentWindowMode = WindowMode.Windowed;
                fullscreen.gameObject.SetActive(false);
                windowed.gameObject.SetActive(true);
                break;
            case WindowMode.Windowed:
                currentWindowMode = WindowMode.Fullscreen;
                windowed.gameObject.SetActive(false);
                fullscreen.gameObject.SetActive(true);
                break;
        }
    }

    public void ExitToMenu()
    {

    }

    public void ExitToDesktop()
    {

    }
}

public enum Language { French, English }

public enum WindowMode { Fullscreen, Windowed }
