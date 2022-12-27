using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private static LevelsManager m_instance;
    public static LevelsManager Instance => m_instance;

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

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (Level level in levels)
        {
            level.Initialize(this);
        }

        activeLevel = startLevel;

        LevelTransition(startLevel, null);
    }

    public void LevelTransition(Level nextlevel, Level previousLevel)
    {
        if (previousLevel != null)
        {
            previousLevel.vCam.Priority--;
        }
        nextlevel.vCam.Priority++;

        activeLevel = nextlevel;
    }
}
