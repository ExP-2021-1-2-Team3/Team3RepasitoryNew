using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public GameObject hp1, hp2, hp3, hands;
    public AudioSource audioSource;
    public RetryBtn retrybtn;
    public static int health;
    const string obsTag = "obs";
    bool ended = false; //»óÈÆÀÌ°¡¾¸.
    void Start()
    { 
        health = 3;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == obsTag)
        {
            health -= 1;
        }
    }
    void Update()
    {
        if (health > 3)
            health = 3;
        switch (health)
        {
            case 3:
                hp1.gameObject.SetActive(true);
                hp2.gameObject.SetActive(true);
                hp3.gameObject.SetActive(true);
                break;

            case 2:
                hp1.gameObject.SetActive(true);
                hp2.gameObject.SetActive(true);
                hp3.gameObject.SetActive(false);
                break;

            case 1:
                hp1.gameObject.SetActive(true);
                hp2.gameObject.SetActive(false);
                hp3.gameObject.SetActive(false);
                break;

            case 0:
                hp1.gameObject.SetActive(false);
                hp2.gameObject.SetActive(false);
                hp3.gameObject.SetActive(false);
                hands.gameObject.SetActive(false);
                break;
        }
        if (health <= 0 && !ended)
        {
            ended = true;
            audioSource.Play();
            retrybtn.Show();
            LoadManagerSH.singleTon.GameEnd();
        }
    }

    
}