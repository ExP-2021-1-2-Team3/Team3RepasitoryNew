using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeControl : MonoBehaviour
{
    public float FadeTime = 0.5f; // Fade효과 재생시간
    public Image fadeImg;
    float time = 0f;

    public void Fade()
    {
        fadeImg.gameObject.SetActive(true);
        StartCoroutine(howFade());
    }
    IEnumerator howFade()
    {
        time = 0f;
        Color color = fadeImg.color;
        while (color.a < 1f)
        {
            time += Time.deltaTime / FadeTime;
            color.a = Mathf.Lerp(0, 1, time);
            fadeImg.color = color;
            yield return null;
        }
        time = 0f;
        yield return new WaitForSeconds(1f);
        while (color.a > 0f)
        {
            time += Time.deltaTime / FadeTime;
            color.a = Mathf.Lerp(1, 0, time);
            fadeImg.color = color;
            yield return null;
        }
        fadeImg.gameObject.SetActive(false);
        yield return null;
    }

}