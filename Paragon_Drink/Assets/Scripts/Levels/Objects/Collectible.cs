using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameManager _gameManager;

    private Animator _animator;
    private Collider2D _collider;

    private EventInstance _idleSound;
    private EventInstance _collectSound;

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        _idleSound = RuntimeManager.CreateInstance("event:/Objects/collectable_idle");
        _idleSound.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        _collectSound = RuntimeManager.CreateInstance("event:/Objects/collectable_collect");

        _idleSound.start();
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

        _idleSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _collectSound.start();

        yield return new WaitForSeconds(0.8f);

        gameObject.SetActive(false);
    }
}
