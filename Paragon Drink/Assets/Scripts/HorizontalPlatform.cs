using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : BreakablePlatform
{
    private int feet = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!broken)
        {
            if (collision.gameObject.CompareTag("Feet"))
            {
                if (collision.transform.parent.GetComponent<FormChanger>().form == Form.Hydrated)
                {
                    feet++;
                    if (feet == 2)
                    {
                        Break();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!broken)
        {
            if (collision.gameObject.CompareTag("Feet"))
            {
                if (collision.transform.parent.GetComponent<FormChanger>().form == Form.Hydrated)
                {
                    feet--;
                }
            }
        }
    }
}
