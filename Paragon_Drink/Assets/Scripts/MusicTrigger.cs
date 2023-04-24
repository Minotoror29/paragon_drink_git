using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    private MusicManager _musicManager;
    [SerializeField] private int musicIndex;

    private bool _activated = false;

    public void Initialize(MusicManager musicManager)
    {
        _musicManager = musicManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeSequence();
    }

    private void ChangeSequence()
    {
        if (_activated) return;

        StartCoroutine(_musicManager.ChangeSequence(musicIndex));
        _activated = true;
    }
}
