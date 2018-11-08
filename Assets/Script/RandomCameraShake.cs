using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCameraShake : MonoBehaviour
{
    public float duration;
    public float magnitude;

    // To identify which plane shaking
    // 1: XY 2: YZ 3: XZ
    private int planeNum;

    public void ShakeXY()
    {
        StartCoroutine(Shake(1, duration, magnitude));
    }

    public void ShakeYZ()
    {
        StartCoroutine(Shake(2, duration, magnitude));
    }

    public void ShakeXZ()
    {
        StartCoroutine(Shake(3, duration, magnitude));
    }

    IEnumerator Shake(int planeNum, float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float a = Random.Range(-1f, 1f) * magnitude;
            float b = Random.Range(-1f, 1f) * magnitude;

            switch (planeNum)
            {
                // XY
                case 1:
                    transform.localPosition = new Vector3(a, b, originalPosition.z);
                    break;
                // YZ
                case 2:
                    transform.localPosition = new Vector3(originalPosition.x, a, b);
                    break;
                // XZ
                case 3:
                    transform.localPosition = new Vector3(a, originalPosition.y, b);
                    break;
            }

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
