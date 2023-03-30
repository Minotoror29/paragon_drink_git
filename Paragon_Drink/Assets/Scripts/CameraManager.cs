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
    private CinemachineVirtualCamera _currentVCam;

    //Shake
    private CinemachineBasicMultiChannelPerlin _perlin;
    private float _shakeTimer = 0f;

    //Zoom
    private float _originalSize;
    private bool _zoom = false;
    private float _zoomSpeed = 1f;

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
    }

    public void CameraTransition(CinemachineVirtualCamera nextCam, CinemachineVirtualCamera currentCam = null)
    {
        nextCam.gameObject.SetActive(true);

        if (currentCam != null)
        {
            currentCam.gameObject.SetActive(false);
        }

        _currentVCam = nextCam;
        _perlin = _currentVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _originalSize = _currentVCam.m_Lens.OrthographicSize;
    }

    public void ShakeCam(float gain, float time)
    {
        _perlin.m_AmplitudeGain = gain;
        _shakeTimer = time;
    }

    public void Zoom(float zoomSpeed)
    {
        _zoom = true;
        _zoomSpeed = zoomSpeed;
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
            _currentVCam.m_Lens.OrthographicSize -= Time.deltaTime * _zoomSpeed;
        } else
        {
            _currentVCam.m_Lens.OrthographicSize = Mathf.Lerp(_currentVCam.m_Lens.OrthographicSize, _originalSize, 0.05f);
        }
    }
}
