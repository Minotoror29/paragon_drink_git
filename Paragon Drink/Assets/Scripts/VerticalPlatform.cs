using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : BreakablePlatform
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<FormChanger>())
        {
            if (collision.gameObject.GetComponent<FormChanger>().dashing)
            {
                Break();
            }
        }
    }
}
