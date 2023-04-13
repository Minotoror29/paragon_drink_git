using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneScreen : MonoBehaviour
{
    private CutsceneManager _cutsceneManager;

    private int _screenIndex;
    protected VideoPlayer _player;
    protected float _screenTimer = 0f;
    private Animator _animator;

    [SerializeField] private GameObject skipButton;

    public void Initialize(CutsceneManager cutsceneManager, int screenIndex)
    {
        _cutsceneManager = cutsceneManager;
        _screenIndex = screenIndex;
        _animator = GetComponent<Animator>();

        _player = GetComponent<VideoPlayer>();
    }

    public void EnterScreen()
    {
        gameObject.SetActive(true);
    }

    public void ExitScreen()
    {
        _animator.CrossFade("Screen" + (_screenIndex + 1) + "_Exit", 0);
    }

    public void SwitchScreen()
    {
        gameObject.SetActive(false);

        _cutsceneManager.SwitchScreen();
    }

    protected void DisplaySkipButton()
    {
        skipButton.SetActive(true);
        _cutsceneManager.DisplaySkipButton();
    }

    public virtual void UpdateLogic()
    {
        if (_screenTimer < 2f)
        {
            _screenTimer += Time.deltaTime;

            if (_screenTimer >= 2f)
            {
                DisplaySkipButton();
            }
        }
    }
}
