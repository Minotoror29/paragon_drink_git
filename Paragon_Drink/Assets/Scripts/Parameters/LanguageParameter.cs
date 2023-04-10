using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageParameter : ParameterValue
{
    [SerializeField] private GameObject french;
    [SerializeField] private GameObject english;

    protected override void ChangeValue()
    {
        if (_gameParameters.language == Language.French)
        {
            french.SetActive(true);
            english.SetActive(false);
        } else if (_gameParameters.language == Language.English)
        {
            english.SetActive(true);
            french.SetActive(false);
        }
    }
}
