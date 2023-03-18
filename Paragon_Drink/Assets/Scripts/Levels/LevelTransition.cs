using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    private LevelsManager _levelsManager;
    private Level _level;
    [SerializeField] private Level nextLevel;

    public void Initialize(LevelsManager levelsManager, Level level)
    {
        _levelsManager = levelsManager;
        _level = level;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_levelsManager.activeLevel != _level)
        {
            return;
        }

        Transition();
    }

    private void Transition()
    {
        _levelsManager.StartCoroutine(_levelsManager.LevelTransition(nextLevel, _level));
    }
}
