using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FinalScreen : CutsceneScreen
{
    public override void UpdateLogic()
    {
        if (_screenTimer < 2f)
        {
            _screenTimer += Time.deltaTime;
            if (_screenTimer >= 2f)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                DisplaySkipButton();
            }
        }
    }
}
