using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Credits : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private Level lastLevel;

    public List<CreditPannel> _pannels = new List<CreditPannel>();
    private int _currentPannelIndex = 0;

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;

        for (int i = 0; i < transform.GetChild(2).childCount; i++)
        {
            CreditPannel pannel = transform.GetChild(2).GetChild(i).GetComponent<CreditPannel>();
            _pannels.Add(pannel);
            pannel.Initialize(_gameManager, this);
        }
    }

    public void StartCredits()
    {
        vCam.gameObject.SetActive(true);
        lastLevel.vCam.gameObject.SetActive(false);

        StartCoroutine(FirstPannel());
    }

    public void SwitchPannel()
    {
        if (_currentPannelIndex < _pannels.Count)
        {
            _pannels[_currentPannelIndex].ActivatePannel();
            _currentPannelIndex++;
        }
    }

    public IEnumerator FirstPannel()
    {
        yield return new WaitForSeconds(1.5f);

        SwitchPannel();
    }
}
