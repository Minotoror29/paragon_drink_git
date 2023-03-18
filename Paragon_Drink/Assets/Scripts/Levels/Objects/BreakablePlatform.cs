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
        }
    }
}
