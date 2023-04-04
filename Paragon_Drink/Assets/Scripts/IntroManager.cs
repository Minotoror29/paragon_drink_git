using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : Manager
{
    [SerializeField] private AnimationClip introAnimation;
    private float _introTimer = 0f;

    public override void Initialize(GameManager gameManager, StateMachine stateMachine)
    {
        base.Initialize(gameManager, stateMachine);

        gameObject.SetActive(true);
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
