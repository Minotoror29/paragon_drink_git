using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Manager
{
    private EventInstance _musicInstance;
    private MusicTrigger[] _triggers;

    [SerializeField] private float sequenceTransitionSpeed = 1f;

    private float _currentIndex;

    [SerializeField] private Transform end;
    [SerializeField] private Transform player;

    public override void Initialize(GameManager gameManager, StateMachine stateMachine)
    {
        base.Initialize(gameManager, stateMachine);

        _triggers = FindObjectsOfType<MusicTrigger>(true);
        foreach (MusicTrigger trigger in _triggers)
        {
            trigger.Initialize(this);
        }

        _musicInstance = RuntimeManager.CreateInstance("event:/Music");
        StartCoroutine(ChangeSequence(1));
    }

    public IEnumerator ChangeSequence(int index)
    {
        if (index == 1)
        {
            _musicInstance.start();
        }

        while (_currentIndex < index)
        {
            _currentIndex += Time.deltaTime * sequenceTransitionSpeed;
            _currentIndex = Mathf.Clamp(_currentIndex, 0, index);
            _musicInstance.setParameterByName("Music Sequence", _currentIndex);
            yield return null;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        FadeMusic();
    }

    public void FadeMusic()
    {
        if (player == null || end == null) return;

        float volume = Mathf.Abs(end.position.x - player.position.x) / 40f;
        volume = Mathf.Clamp(volume, 0f, 1f);

        _musicInstance.setVolume(volume);
    }
}
