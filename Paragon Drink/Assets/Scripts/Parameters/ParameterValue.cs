using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParameterValue : MonoBehaviour
{
    protected GameParameters _gameParameters;

    public virtual void Initialize(GameParameters gameParameters)
    {
        _gameParameters = gameParameters;

        ChangeValue();
    }

    protected abstract void ChangeValue();
}
