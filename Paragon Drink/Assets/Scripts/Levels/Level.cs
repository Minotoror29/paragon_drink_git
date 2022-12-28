using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private LevelsManager _levelsManager;
    [HideInInspector] public CinemachineVirtualCamera vCam;

    private List<LevelTransition> transitions = new List<LevelTransition>();

    public void Initialize(LevelsManager levelsManager)
    {
        _levelsManager = levelsManager;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>(true);

        transitions = new List<LevelTransition>();
        foreach (LevelTransition transition in GetComponentsInChildren<LevelTransition>())
        {
            transitions.Add(transition);
        }

        foreach (LevelTransition transition in transitions)
        {
            transition.Initialize(_levelsManager, this);
        }
    }
}
