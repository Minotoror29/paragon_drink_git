using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : Manager
{
    private static LevelsManager instance;
    public static LevelsManager Instance => instance;

    private CameraManager _cameraManager;

    [SerializeField] private List<Level> levels;
    [HideInInspector] public Level activeLevel;
    [SerializeField] private Level startLevel;

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

        _cameraManager = CameraManager.Instance;

        foreach (Level level in levels)
        {
            level.Initialize(_gameManager, this);
        }

        activeLevel = startLevel;

        StartCoroutine(LevelTransition(startLevel, null));
    }

    public IEnumerator LevelTransition(Level nextlevel, Level previousLevel)
    {
        if (previousLevel != null)
        {
            _cameraManager.CameraTransition(nextlevel.vCam, previousLevel.vCam);

            activeLevel = nextlevel;

            StateMachine.Instance.ChangeState(new TransitionState());

            yield return new WaitForSeconds(0.5f);

            StateMachine.Instance.ChangeState(new PlayState());
        } else
        {
            _cameraManager.CameraTransition(nextlevel.vCam);
        }        
    }
}
