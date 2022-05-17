using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFramerate : MonoBehaviour
{
    [SerializeField, Range(-1, 120)] private int fps = -1;

    private void Update()
    {
        Application.targetFrameRate = fps;
    }
}
