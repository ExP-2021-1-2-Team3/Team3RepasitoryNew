using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float maxSpeed;
    public float jumpPower;
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        rigid.velocity = new Vector2(speed, rigid.velocity.y);
        
        if(Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("오른쪽 화살표 눌림");
            anim.SetBool("isMoving", true);
            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            else
                speed = maxSpeed;            
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("왼쪽 화살표 눌림");
            anim.SetBool("isMoving", true);
            if (speed > (-1) * maxSpeed)
            {
                speed -= acceleration * Time.deltaTime;
            }
            else
                speed = (-1) * maxSpeed;
        }

        if(!Input.GetKey(KeyCode.RightArrow)&& !Input.GetKey(KeyCode.LeftArrow))
        {
            speed = 0f;
            anim.SetBool("isMoving", false);
        }

        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        if (rigid)
        {
            if (Input.GetButtonUp("Horizontal"))
            {
                //버튼 뗐을 때
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }
        }

        //방향전환
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        if (rigid.velocity.normalized.x == 0)
            anim.SetBool("isMoving", false);
        else
            anim.SetBool("isMoving", true);

    }

    void OnCollisionStay2D(Collision2D other)       //점프상태인지 아닌지를 판단해줌. 이게 있어야 버튼 누를때 언제 점프인지를 안다.
    {
        anim.SetBool("isJumping", false);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        anim.SetBool("isJumping", true);
    }

    public void FixedUpdate()
    {
        //rigid.AddForce(Vector2.right * h * maxSpeed, ForceMode2D.Impulse);

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 2f)
                {
                    anim.SetBool("isJumping", false);
                }
            }
        }
    }
    
    

}
*/