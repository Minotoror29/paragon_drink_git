using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private Transform levels;
    private Transform activeLevel;
    [SerializeField] private int startLevel;

    private void Start()
    {
        activeLevel = levels.GetChild(startLevel);
        Camera.main.GetComponent<CameraTransition>().Transition(activeLevel);
    }

    public void LevelTransition(Transform level)
    {
        activeLevel = level;

        for (int i = 0; i < levels.childCount; i++)
        {
            if (levels.GetChild(i) != activeLevel)
            {
                levels.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                levels.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
