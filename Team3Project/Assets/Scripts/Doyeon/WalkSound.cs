using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    //[SerializeField] float speed = 5.0f;
    float MoveDYX, MoveDYY;
    public VirtualJoystick joystick;
    Rigidbody2D rb;
    AudioSource audioSrc;
    //bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        MoveDYX = joystick.inputDirection.x;
        MoveDYY = joystick.inputDirection.y;
        
        if ((MoveDYX == 0) && (MoveDYY == 0)){
            audioSrc.Stop();
        }else{
            if(!audioSrc.isPlaying){
                audioSrc.Play();
                }
            
        }
        
    }

    
}
