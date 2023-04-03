using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FinalScreen : CutsceneScreen
{
    public override void UpdateLogic()
    {
        if (_screenTimer < _player.clip.length)
        {
            _screenTimer += Time.deltaTime;
            if (_screenTimer >= _player.clip.length)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                DisplaySkipButton();
            }
        }
    }
}
