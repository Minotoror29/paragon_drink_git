using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private bool broken = false;
    [SerializeField] private bool canRespawn;
    [SerializeField] private float respawnTime;
    private float t = 0;

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
                transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                sprite.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!broken)
        {
            if (collision.gameObject.GetComponent<FormChanger>())
            {
                if (collision.gameObject.GetComponent<FormChanger>().form == Form.Hydrated)
                {
                    transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    sprite.enabled = false;
                    if (canRespawn)
                    {
                        t = respawnTime;
                        broken = true;
                    }
                }
            }
        }
    }
}
