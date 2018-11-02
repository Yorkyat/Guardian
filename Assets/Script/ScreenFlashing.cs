using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlashing : MonoBehaviour
{
    public Image flashingImage;
    public Color flashingColor;
    public float flashingSpeed = 5f;
    public bool flashingCondition = false;
    public int incrementCounter = 0;

    private Color incrementColor = Color.clear;

    void Update()
    {
        if (flashingCondition)
        {
            flashingImage.color = incrementColor + flashingColor;
        }
        else
        {
            flashingImage.color = Color.Lerp(flashingImage.color, incrementColor, flashingSpeed * Time.deltaTime);
        }
        flashingCondition = false;
    }

    public void Increment()
    {
        incrementCounter++;
        incrementColor += flashingColor;
    }
}
