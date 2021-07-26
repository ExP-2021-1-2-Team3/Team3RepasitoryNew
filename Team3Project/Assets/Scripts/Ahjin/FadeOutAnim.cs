using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAnim : MonoBehaviour
{
    float time;
    public GameObject character;
    public float _fadeTime = 1f;

     void Update()
    {
        if (time < _fadeTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1- time/_fadeTime);
        }
        else
        {
            time = 0;
            character.gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }
}
