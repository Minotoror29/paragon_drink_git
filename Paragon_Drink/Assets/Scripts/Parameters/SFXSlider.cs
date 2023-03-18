using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : ParameterValue
{
    private Slider _slider;

    public override void Initialize(GameParameters gameParameters)
    {
        _slider = GetComponent<Slider>();

        base.Initialize(gameParameters);
    }

    protected override void ChangeValue()
    {
        _slider.value = _gameParameters._sfxVolume;
    }
}
