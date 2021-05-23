using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDY : MonoBehaviour
{
    float MoveDYX, MoveDYY;
    public float speed = 5.0f;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveDYX = Input.GetAxis("Horizontal");
        MoveDYY = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(MoveDYX * speed, MoveDYY * speed);
    }
}
