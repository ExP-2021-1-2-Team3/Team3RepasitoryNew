using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDY : MonoBehaviour
{
    [SerializeField] Animator anim;
    float MoveDYX, MoveDYY;
    public VirtualJoystick joystick;
    public float speed = 5.0f;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveDYX = joystick.inputDirection.x;
        MoveDYY = joystick.inputDirection.y;
        
        if ((MoveDYX == 0) && (MoveDYY == 0)){
            anim.SetInteger("ManState", 1);
        }else{
            anim.SetInteger("ManState", 0);
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(MoveDYX * speed, MoveDYY * speed);
    }
}
