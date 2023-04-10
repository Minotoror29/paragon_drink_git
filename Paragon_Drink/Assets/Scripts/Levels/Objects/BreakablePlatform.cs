using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private Collider2D coll;

    [HideInInspector] public bool broken = false;
    public bool canRespawn;
    public float respawnTime;
    [HideInInspector] public float t = 0;

    private EventInstance _openSound;
    private EventInstance _closeSound;
    private EventInstance _breakSound;

    private Animator _animator;

    private void Start()
    {
        _openSound = RuntimeManager.CreateInstance("event:/Objects/fence_open");
        _closeSound = RuntimeManager.CreateInstance("event:/Objects/fence_close");
        _breakSound = RuntimeManager.CreateInstance("event:/Objects/fence_break");
        _closeSound.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));

        _animator = GetComponent<Animator>();
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
                Close();
            }
        }
    }

    public void Break()
    {
        coll.enabled = false;
        _animator.CrossFade("Barriere_Open", 0);
        _openSound.start();
        if (canRespawn)
        {
            t = respawnTime;
            broken = true;
        }
    }

    private void Close()
    {
        _animator.CrossFade("Barriere_Close", 0);
        broken = false;
        coll.enabled = true;
        _closeSound.start();
    }
}
