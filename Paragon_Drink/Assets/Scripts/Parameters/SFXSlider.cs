using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : ParameterValue
{
    private Slider _slider;
    private EventInstance _sliderSound;

    public override void Initialize(GameParameters gameParameters)
    {
        _slider = GetComponent<Slider>();
        _sliderSound = RuntimeManager.CreateInstance("event:/UI/ui_casual_countup");

        base.Initialize(gameParameters);
    }

    protected override void ChangeValue()
    {
        _slider.value = _gameParameters._sfxVolume;
    }


    private float _lastValue;

    private void Update()
    {
        if (_slider.value != _lastValue)
        {
            _sliderSound.start();
        } else
        {
            _sliderSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        _lastValue = _slider.value;
    }
}
