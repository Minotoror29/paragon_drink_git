using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    private LevelsManager lm;

    [SerializeField] private float smoothTime;
    private Vector3 velocity;

    private Transform targetLevel;

    private void Awake()
    {
        lm = FindObjectOfType<LevelsManager>();
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(targetLevel.position.x, targetLevel.position.y, -10f),
            ref velocity,
            smoothTime);
    }

    public void Transition(Transform target)
    {
        targetLevel = target;
        lm.LevelTransition(target);
    }
}
