using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditorInternal;

public class CreditsManager : Manager
{
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private Level lastLevel;

    public List<CreditPannel> _pannels = new List<CreditPannel>();
    private int _currentPannelIndex = 0;

    public override void Initialize(GameManager gameManager, StateMachine stateMachine)
    {
        base.Initialize(gameManager, stateMachine);

        for (int i = 0; i < transform.GetChild(2).childCount; i++)
        {
            CreditPannel pannel = transform.GetChild(2).GetChild(i).GetComponent<CreditPannel>();
            _pannels.Add(pannel);
            pannel.Initialize(_gameManager, this);
        }
    }

    public void StartCredits()
    {
        _stateMachine.ChangeState(new MenuState());

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
