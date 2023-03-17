using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class GameParameters : MonoBehaviour
{
    private Bus _sfxBus;
    public float _sfxVolume;
    [SerializeField] private float sfxVolumeDefaultValue;

    private Bus _musicBus;
    public float _musicVolume;
    [SerializeField] private float musicVolumeDefaultValue;

    public Language _language;
    [SerializeField] private Language defaultLanguage;

    private ParameterValue[] _parameterValues;

    public void Initialize()
    {
        _sfxBus = RuntimeManager.GetBus("bus:/SFX");
        _musicBus = RuntimeManager.GetBus("bus:/Music");

        _sfxVolume = sfxVolumeDefaultValue;
        _musicVolume = musicVolumeDefaultValue;
        _language = defaultLanguage;
    }

    public void InitializeParameters()
    {
        _parameterValues = FindObjectsOfType<ParameterValue>(true);
        foreach (ParameterValue parameter in _parameterValues)
        {
            parameter.Initialize(this);
        }
    }

    public void ChangeSFXVolume(float value)
    {
        _sfxVolume = value;
        _sfxBus.setVolume(DecibelToLinear(_sfxVolume));
    }

    public void ChangeMusicVolume(float value)
    {
        _musicVolume = value;
        _musicBus.setVolume(DecibelToLinear(_musicVolume));
    }

    public void ChangeLanguage(Language language)
    {
        _language = language;
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }
}
