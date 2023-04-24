using Cinemachine;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Manager
{
    private static CameraManager instance;
    public static CameraManager Instance => instance;

    private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _currentVCam;

    //Shake
    private CinemachineBasicMultiChannelPerlin _perlin;
    private float _shakeTimer = 0f;

    //Zoom
    private float _originalSize;
    private bool _zoom = false;
    [SerializeField] private AnimationCurve _zoomCurve;
    private float _zoomTimer = 0f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public override void Initialize(GameManager gameManager, StateMachine stateMachine)
    {
        base.Initialize(gameManager, stateMachine);

        _mainCamera = Camera.main;

        SetCameraVariables();
    }

    private void SetCameraVariables()
    {
        if (_currentVCam == null) return;

        _perlin = _currentVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _originalSize = _currentVCam.m_Lens.OrthographicSize;
    }

    public void CameraTransition(CinemachineVirtualCamera nextCam, CinemachineVirtualCamera currentCam = null)
    {
        nextCam.gameObject.SetActive(true);

        if (currentCam != null)
        {
            _perlin.m_AmplitudeGain = 0f;
            currentCam.gameObject.SetActive(false);
        }

        _currentVCam = nextCam;

        SetCameraVariables();
    }

    public void ShakeCam(float gain, float time)
    {
        _perlin.m_AmplitudeGain = gain;
        _shakeTimer = time;
    }

    public void Zoom()
    {
        _zoom = true;
    }

    public void BackToOriginalSize()
    {
        _zoom = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        //Shake
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;

            if (_shakeTimer <= 0)
            {
                _perlin.m_AmplitudeGain = 0f;
            }
        }

        //Zoom
        if (_zoom)
        {
            if (_zoomTimer < _zoomCurve.keys[_zoomCurve.length - 1].time)
            {
                _zoomTimer += Time.deltaTime;
                _currentVCam.m_Lens.OrthographicSize = _originalSize - _zoomCurve.Evaluate(_zoomTimer);
            }
        } else
        {
            _currentVCam.m_Lens.OrthographicSize = Mathf.MoveTowards(_currentVCam.m_Lens.OrthographicSize, _originalSize, 0.0025f);
            _zoomTimer = 0f;
        }
    }
}
