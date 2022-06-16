using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] private Transform nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transition();
    }

    private void Transition()
    {
        Camera.main.GetComponent<CameraTransition>().Transition(nextLevel);
    }
}
