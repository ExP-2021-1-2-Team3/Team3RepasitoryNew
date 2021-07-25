using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerJH : MonoBehaviour
{
    public GameObject audioMove;
    public GameObject audioInteract;
    public AudioSource moveSound;
    public AudioSource InteractSound;
    public Player player;
    public Btnclick button;
    

    public void Start()
    {
        moveSound = audioMove.GetComponent<AudioSource>();
        InteractSound = audioInteract.GetComponent<AudioSource>();
    }

    public void playSound()
    {
        if (player.anim.GetBool("isMoving") == true && player.anim.GetBool("isJumping") == false)
            moveSound.Play();

        if (button.canPlayIntSound)
            InteractSound.Play();
        
    }
}
