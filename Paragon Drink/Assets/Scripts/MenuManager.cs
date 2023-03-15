using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private MenuControls _menuControls;

    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Image fullscreen;
    [SerializeField] private Image windowed;

    private WindowMode currentWindowMode = WindowMode.Fullscreen;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _menuControls = new MenuControls();
        _menuControls.Menu.QuitMenu.performed += ctx => HideOptionsMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DisplayOptionsMenu()
    {
        optionsMenu.SetActive(true);
        _menuControls.Menu.Enable();
    }
    
    public void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
        _menuControls.Menu.Disable();
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
