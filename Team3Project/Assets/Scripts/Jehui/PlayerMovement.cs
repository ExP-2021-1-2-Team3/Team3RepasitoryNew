using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        //점프       
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


    public void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h * maxSpeed, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) //우측 최대 속도
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < (-1) * maxSpeed) //좌측 최대 속도
            rigid.velocity = new Vector2((-1) * maxSpeed, rigid.velocity.y);

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
