using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public SpriteRenderer sprite;
    [SerializeField] private Collider2D coll;

    [HideInInspector] public bool broken = false;
    public bool canRespawn;
    public float respawnTime;
    [HideInInspector] public float t = 0;

    private EventInstance _openSound;
    private EventInstance _closeSound;
    private EventInstance _breakSound;

    private void Start()
    {
        _openSound = RuntimeManager.CreateInstance("event:/Objects/fence_open");
        _closeSound = RuntimeManager.CreateInstance("event:/Objects/fence_close");
        _breakSound = RuntimeManager.CreateInstance("event:/Objects/fence_break");
        _closeSound.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
    }

    private void Update()
    {
        if (broken)
        {
            if (t > 0)
            {
                t -= Time.deltaTime;
            }
            else
            {
                broken = false;
                coll.enabled = true;
                sprite.enabled = true;
                _closeSound.start();
            }
        }
    }

    public void Break()
    {
        coll.enabled = false;
        sprite.enabled = false;
        if (canRespawn)
        {
            t = respawnTime;
            broken = true;
            _openSound.start();
        } else
        {
            _breakSound.start();
        }
    }
}
