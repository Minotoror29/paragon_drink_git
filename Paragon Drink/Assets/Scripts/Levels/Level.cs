using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private GameManager _gameManager;
    private LevelsManager _levelsManager;
    [HideInInspector] public CinemachineVirtualCamera vCam;

    private LevelTransition[] _transitions;

    private Collectible[] _collectibles;

    public void Initialize(GameManager gameManager, LevelsManager levelsManager)
    {
        _gameManager = gameManager;
        _levelsManager = levelsManager;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>(true);

        _transitions = GetComponentsInChildren<LevelTransition>();
        foreach (LevelTransition transition in _transitions)
        {
            transition.Initialize(_levelsManager, this);
        }

        _collectibles = GetComponentsInChildren<Collectible>();
        foreach (Collectible collectible in _collectibles)
        {
            collectible.Initialize(_gameManager);
        }
    }
}
