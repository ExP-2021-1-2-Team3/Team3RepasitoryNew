using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchObjectFloor : MonoBehaviour
{
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -4)
        {
            rigid.velocity = Vector2.zero;
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        }
        if(transform.position.x > 9)
        {
            rigid.velocity = Vector2.zero;
            transform.position = new Vector3(9,transform.position.y, transform.position.z);
        }
        if (transform.position.x < -9)
        {
            rigid.velocity = Vector2.zero;
            transform.position = new Vector3(-9, transform.position.y, transform.position.z);
        }
    }
}
