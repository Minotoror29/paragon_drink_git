using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private int nextLevel;
    [SerializeField] private int nextLevelSpawnIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            SceneTransition.spawnPointIndex = nextLevelSpawnIndex;
            SceneManager.LoadScene("Level_" + nextLevel);
        }
    }
}
