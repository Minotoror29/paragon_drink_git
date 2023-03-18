using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Update()
    {
        transform.position = player.transform.position;
    }
}
