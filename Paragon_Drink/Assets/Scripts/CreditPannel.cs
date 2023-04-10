using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditPannel : MonoBehaviour
{
    protected GameManager _gameManager;
    private CreditsManager _credits;
    protected GameParameters _gameParameters;

    protected TextMeshProUGUI _text;
    private float _fadeSpeed = 0.5f;
    private float _pannelTime = 5f;

    public void Initialize(GameManager gameManager, CreditsManager credits, GameParameters gameParameters)
    {
        _gameManager = gameManager;
        _credits = credits;
        _gameParameters = gameParameters;
    }

    public virtual void ActivatePannel()
    {
        gameObject.SetActive(true);
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0);
        StartCoroutine(Activatepannel());
    }

    private IEnumerator Activatepannel()
    {
        StartCoroutine(FadeInText());

        yield return new WaitForSeconds(_pannelTime);

        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeInText()
    {
        while (_text.color.a < 1f)
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a + Time.deltaTime * _fadeSpeed);
            yield return null;
        }
    }

    private IEnumerator FadeOutText()
    {
        while (_text.color.a > 0f)
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - Time.deltaTime * _fadeSpeed);
            yield return null;
        }

        gameObject.SetActive(false);
        _credits.SwitchPannel();
    }
}
