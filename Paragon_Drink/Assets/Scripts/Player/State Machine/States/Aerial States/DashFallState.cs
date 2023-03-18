using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFallState : AerialState
{
    private float _timer = 0.1f;
    private bool _canBreakPlatforms = true;

    public DashFallState(PlayerStateMachine playerStateMachine, PlayerController playerController, Animator animator) : base(playerStateMachine, playerController, animator)
    {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_jumpInput && !_playerController.requireNewJumpPress)
            {
                _currentSuperState.ChangeSubState(new DashJumpState(_playerStateMachine, _playerController, _animator));
            }
        } else
        {
            _canBreakPlatforms = false;
        }
    }

    public override void UpdatePhysics()
    {
        _playerController.Fall();
    }

    public override void EnterCollision(Collision2D collision)
    {
        base.EnterCollision(collision);

        if (_canBreakPlatforms)
        {
            VerticalPlatform platform = collision.gameObject.GetComponent<VerticalPlatform>();

            if (platform != null)
            {
                platform.Break();
            }
        }
    }
}
