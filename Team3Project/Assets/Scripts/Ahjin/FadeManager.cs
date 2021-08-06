using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    float time;
    public float _fadeTime = 1f;

    public void FadeOut()
    {
        if (time < _fadeTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f - time/_fadeTime);
        }
        else
        {
            time = 0;
            gameObject.SetActive(false);
            LoadManagerSH.singleTon.LoadGameScene();
        }
        time += Time.deltaTime;

        }

    public void FadeIn()
    {
        if (time < _fadeTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time/_fadeTime);
        }
        time += Time.deltaTime;
    }
}
