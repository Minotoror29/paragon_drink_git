using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameManager _gameManager;

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            _gameManager.CollectItem();
            gameObject.SetActive(false);
        }
    }
}
