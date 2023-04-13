using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuanButton : CustomButton
{
    private Animator _animator;
    private bool _crack = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Select()
    {
        base.Select();

        if (_crack ) return;

        _animator.CrossFade("Play-Selected", 0f);
    }

    public override void Deselect()
    {
        base.Deselect();

        if (_crack) return;

        _animator.CrossFade("Play-Idle", 0f);
    }

    public void Crack()
    {
        if (_crack ) return;

        _crack = true;
        _animator.CrossFade("Play-Activated", 0f);
    }
}
