using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : BreakablePlatform
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!broken)
        {
            if (collision.gameObject.GetComponent<FormChanger>())
            {
                if (collision.gameObject.GetComponent<FormChanger>().form == Form.Hydrated)
                {
                    Break();
                }
            }
        }
    }
}
