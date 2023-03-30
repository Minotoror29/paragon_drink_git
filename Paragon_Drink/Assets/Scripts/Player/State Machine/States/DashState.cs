using Cinemachine;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private float _dashTime;

    public DashState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
        _soundPath = "event:/Player/juan_dash_impact";
    }

    public override void Enter(State previousState, State superState)
    {
        base.Enter(previousState, superState);

        _animator.CrossFade("Dash", 0f);

        _dashTime = _playerController.dashTime;

        _playerController.CreateBubble();

        CameraManager.Instance.ShakeCam(5f, 0.25f);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_dashTime > 0f)
        {
            _dashTime -= Time.deltaTime;
            _playerController.Dash();
        } else
        {
            _currentSuperState.ChangeSubState(new DashFallState(_playerStateMachine, _playerController, _animator));
        }

        if (_jumpInput && !_playerController.requireNewJumpPress)
        {
            _currentSuperState.ChangeSubState(new DashJumpState(_playerStateMachine, _playerController, _animator));
        }
    }

    public override void EnterCollision(Collision2D collision)
    {
        base.EnterCollision(collision);

        VerticalPlatform platform = collision.gameObject.GetComponent<VerticalPlatform>();

        if (platform != null)
        {
            platform.Break();
        }
    }
}
