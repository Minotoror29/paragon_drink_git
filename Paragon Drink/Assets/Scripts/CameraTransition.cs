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
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(targetLevel.position.x, targetLevel.position.y, -10f),
            smoothTime);

        if (Time.timeScale < 1)
        {
            if (transform.position == new Vector3(targetLevel.position.x, targetLevel.position.y, -10f))
            {
                Time.timeScale = 1;
            }
        }
    }

    public void Transition(Transform target)
    {
        targetLevel = target;
        lm.LevelTransition(target);
        GetComponent<Camera>().orthographicSize = targetLevel.GetComponent<Level>().size;

        Time.timeScale = 0;
    }
}
