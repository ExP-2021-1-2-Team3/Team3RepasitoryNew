using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsHS : MonoBehaviour
{
    private static AudioSource audioSource;
    private static AudioClip effect1, effect2;
    private static SoundEffectsHS self;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        effect1 = Resources.Load<AudioClip>("Heesu/PlaceHolderEFFECT1");
        effect2 = Resources.Load<AudioClip>("Heesu/PlaceHolderEFFECT2");
        self = this;
    }

    public static void BgmStart(){
        self.ActualStart();
    }

    public void ActualStart(){
        StartCoroutine(WaitForSceneLoad());
    }

    IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(2.0f);
        audioSource.Play();
    }

    public static void BgmStop(){
        audioSource.Stop();
    }

    public static void LetterInput()
    {
        audioSource.PlayOneShot(effect1);
    }

    public static void FinishSound()
    {
        audioSource.PlayOneShot(effect2);
    }

}
