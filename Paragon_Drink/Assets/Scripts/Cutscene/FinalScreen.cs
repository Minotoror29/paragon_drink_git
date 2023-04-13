using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

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
