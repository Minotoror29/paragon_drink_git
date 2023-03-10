using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private static LevelsManager m_instance;
    public static LevelsManager Instance => m_instance;

    private GameManager _gameManager;

    [SerializeField] private List<Level> levels;
    [HideInInspector] public Level activeLevel;
    [SerializeField] private Level startLevel;

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this);
        }
        else
        {
            m_instance = this;
        }
    }

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;

        foreach (Level level in levels)
        {
            level.Initialize(_gameManager, this);
        }

        activeLevel = startLevel;

        LevelTransition(startLevel, null);
    }

    public IEnumerator LevelTransition(Level nextlevel, Level previousLevel)
    {
        nextlevel.vCam.gameObject.SetActive(true);
        if (previousLevel != null)
        {
            previousLevel.vCam.gameObject.SetActive(false);
        }

        activeLevel = nextlevel;

        StateMachine.Instance.ChangeState(new TransitionState());

        yield return new WaitForSeconds(0.5f);

        StateMachine.Instance.ChangeState(new PlayState());
    }
}
