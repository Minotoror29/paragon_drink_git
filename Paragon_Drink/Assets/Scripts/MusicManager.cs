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

    public float _currentIndex;

    public override void Initialize(GameManager gameManager, StateMachine stateMachine)
    {
        base.Initialize(gameManager, stateMachine);

        _triggers = FindObjectsOfType<MusicTrigger>(true);
        foreach (MusicTrigger trigger in _triggers)
        {
            trigger.Initialize(this);
        }

        _musicInstance = RuntimeManager.CreateInstance("event:/Music");
        //ChangeSequence(0);
        StartCoroutine(ChangeSequence(0));
    }

    //public void ChangeSequence(int index)
    //{
    //    _musicInstance.setParameterByName("Music Sequence", index);
    //}

    public IEnumerator ChangeSequence(int index)
    {
        if (index == 1)
        {
            _musicInstance.start();
        }

        while (_currentIndex < index)
        {
            _currentIndex += Time.deltaTime * sequenceTransitionSpeed;
            _musicInstance.setParameterByName("Music Sequence", _currentIndex);
            yield return null;
        }
    }
}
