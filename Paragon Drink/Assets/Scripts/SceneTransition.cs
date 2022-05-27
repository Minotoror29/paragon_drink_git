using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public static int spawnPointIndex;
    [SerializeField] private Transform spawnPoints;

    private Transform player;

    private void Start()
    {
        player = PlayerMovement.Instance.transform;

        player.position = spawnPoints.GetChild(spawnPointIndex).transform.position;
    }
}
