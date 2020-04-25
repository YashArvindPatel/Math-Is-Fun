using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBall : MonoBehaviour
{
    public SpriteRenderer ball1, ball2, ball3;

    public void Selected(SpriteRenderer ball)
    {
        if (ball == ball1)
        {
            ball1.color = new Color32(255, 255, 255, 255);
            ball2.color = new Color32(255, 143, 134, 255);
            ball3.color = new Color32(255, 143, 134, 255);
        }
        else if (ball == ball2)
        {
            ball2.color = new Color32(255, 255, 255, 255);
            ball1.color = new Color32(255, 143, 134, 255);
            ball3.color = new Color32(255, 143, 134, 255);
        }
        else if (ball == ball3)
        {
            ball3.color = new Color32(255, 255, 255, 255);
            ball1.color = new Color32(255, 143, 134, 255);
            ball2.color = new Color32(255, 143, 134, 255);
        }
    }
}
