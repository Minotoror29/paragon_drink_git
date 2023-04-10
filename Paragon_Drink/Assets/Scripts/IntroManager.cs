using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : Manager
{
    [SerializeField] private AnimationClip introAnimation;
    private float _introTimer = 0f;

    private EventInstance _introSound;

    public override void Initialize(GameManager gameManager, StateMachine stateMachine)
    {
        base.Initialize(gameManager, stateMachine);

        gameObject.SetActive(true);

        _introSound = RuntimeManager.CreateInstance("event:/Cutscene/intro");
        _introSound.start();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_introTimer < introAnimation.length)
        {
            _introTimer += Time.deltaTime;

            if (_introTimer >= introAnimation.length )
            {
                _stateMachine.ChangeState(new PlayState());
            }
        }
    }
}
