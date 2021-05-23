using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip audioClip1, audioClip2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip1 = Resources.Load<AudioClip>("Item1");
        audioClip2 = Resources.Load<AudioClip>("Nice1");
    }

    public static void ItemdySound()
    {
        audioSource.PlayOneShot(audioClip1);
    }

    public static void FinishSound()
    {
        audioSource.PlayOneShot(audioClip2);
    }
}
