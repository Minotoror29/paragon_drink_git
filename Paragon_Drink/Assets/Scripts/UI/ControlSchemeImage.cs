using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ControlSchemeImage : MonoBehaviour
{
    private MenuControls _menuControls;
    private PlayerInput _menuInput;
    private string _currentControlScheme;

    private GameObject _currentImage;
    [SerializeField] private GameObject keyboardImage; 
    [SerializeField] private GameObject xboxImage; 
    [SerializeField] private GameObject playstationImage; 

    public void Initialize(MenuControls menuControls, PlayerInput menuInput)
    {
        _menuControls = menuControls;
        _menuInput = menuInput;

        _currentImage = keyboardImage;
    }

    private void Update()
    {
        if (_currentControlScheme != _menuInput.currentControlScheme)
        {
            _currentControlScheme = _menuInput.currentControlScheme;
            if (_currentControlScheme == _menuControls.KeyboardScheme.name)
            {
                ChangeDisplayedImage(keyboardImage);
            } else if (_currentControlScheme == _menuControls.XboxScheme.name)
            {
                ChangeDisplayedImage(xboxImage);
            } else if (_currentControlScheme == _menuControls.PS4Scheme.name)
            {
                ChangeDisplayedImage(playstationImage);
            }
        }
    }

    private void ChangeDisplayedImage(GameObject image)
    {
        if (_currentImage != null)
        {
            _currentImage.SetActive(false);
        }

        image.SetActive(true);
        _currentImage = image;
    }
}
