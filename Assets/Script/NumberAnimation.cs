using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAnimation : MonoBehaviour
{

    public Text numberText;
    public float animationTime = 1.5f;

    private float desNum;
    private float initNum;
    private float curNum;

    // Use this for initialization
    void Start()
    {
        if(numberText != null)
        {
            if(numberText.text != "")
            {
                curNum = float.Parse(numberText.text);
            }
            else
            {
                curNum = 0f;
            }
        }
    }

    public void SetTextObject(Text obj)
    {
        numberText = obj;
        if (numberText.text != "")
        {
            curNum = float.Parse(numberText.text);
        }
        else
        {
            curNum = 0f;
        }
    }

    public IEnumerator SetNum(float val, string format)
    {
        initNum = curNum;
        desNum = val;
        yield return StartCoroutine(Animate(format));
    }

    public IEnumerator AddToNum(float val, string format)
    {
        initNum = curNum;
        desNum += val;
        StartCoroutine(Animate(format));
        yield return null;
    }

    public IEnumerator Animate(string format)
    {
        numberText.text = initNum.ToString(format);
        while (curNum != desNum)
        {
            if (initNum < desNum)
            {
                curNum += (animationTime * Time.deltaTime) * (desNum - initNum);
                if (curNum >= desNum)
                {
                    curNum = desNum;
                }
            }
            else
            {
                curNum -= (animationTime * Time.deltaTime) * (initNum - desNum);
                if (curNum <= desNum)
                {
                    curNum = desNum;
                }
            }
            yield return null;

            numberText.text = curNum.ToString(format);
        }
    }
}
