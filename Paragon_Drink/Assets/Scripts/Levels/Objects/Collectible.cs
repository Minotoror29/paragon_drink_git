using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameManager _gameManager;

    private Animator _animator;
    private Collider2D _collider;

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            StartCoroutine(Collect());
        }
    }

    private IEnumerator Collect()
    {
        _collider.enabled = false;
        _gameManager.CollectItem();
        _animator.CrossFade("Collectable-Explosion", 0);

        yield return new WaitForSeconds(0.8f);

        gameObject.SetActive(false);
    }
}
