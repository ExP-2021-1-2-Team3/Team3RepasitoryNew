using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer sRenderer;
    public Rigidbody2D rigid;
    public float speed; //이동 속도
    public float jumpPower; //점프력
    public float horizontalVec, verticalVec; 
    public bool isJump;
    

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();                                        //player의 움직임 표현해야 함.
        sRenderer = GetComponent<SpriteRenderer>();                                 //player에서 이동방향에 따른 좌우반전시켜줌.
        anim = GetComponent<Animator>();                                            //player의 state에 따른 animation 조정.
    }

    void Update()
    {
        Vector2 moveVec = new Vector2(horizontalVec, 0);
        Vector2 jumpVec = !isJump ? Vector2.up : Vector2.zero;

        //플레이어 방향전환
        if (rigid.velocity.x < 0)
            sRenderer.flipX = true;
        else if(rigid.velocity.x > 0)
            sRenderer.flipX = false;
    }
    void OnCollisionStay2D(Collision2D other)
    {
        isJump = false;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isJump = true;
    }

}
