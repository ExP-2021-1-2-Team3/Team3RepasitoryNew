using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;               //플레이어의 애니메이션 관장
    public SpriteRenderer sRenderer;    //플레이어의 스프라이트렌더러 관장
    public Rigidbody2D rigid;           //플레이어 강체화
    public BgSoundManagerJH sound;
    public float jumpPower;             //플레이어 점프시 가할 힘 설정 필요
    public float horizontalVec;         //필요한건 수평방향 벡터만 설정해주면 됨.
    public float acceleration = 1.5f;
    public float Maxvel = 15f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();                                        //player의 움직임 표현해야 함.
        sRenderer = GetComponent<SpriteRenderer>();                                 //player에서 이동방향에 따른 좌우반전시켜줌.
        anim = GetComponent<Animator>();                                            //player의 state에 따른 animation 조정.
    }

    void Update()
    {
        rigid.velocity = new Vector2(horizontalVec, rigid.velocity.y);

        //플레이어 방향전환
        if (rigid.velocity.x < 0)
            sRenderer.flipX = true;
        else if (rigid.velocity.x > 0)
            sRenderer.flipX = false;

        

        if (Btnclick.isRightBtnDown)
        {
            anim.SetBool("isMoving", true);
            if (horizontalVec < Maxvel)
            {
                horizontalVec += acceleration * Time.deltaTime;
            }
            else
                horizontalVec = Maxvel;
        }


        if (Btnclick.isLeftBtnDown)
        {
            anim.SetBool("isMoving", true);
            if (horizontalVec > (-1) * Maxvel)
            {
                horizontalVec -= acceleration * Time.deltaTime;
            }
            else
                horizontalVec = (-1) * Maxvel;                             //최대 속력은 결국 Maxspeed * speed이다. 
        }
        
        if(!Btnclick.isLeftBtnDown && !Btnclick.isRightBtnDown)
        {
            horizontalVec = 0f;
            anim.SetBool("isMoving", false);
        }

    }
    // 오른쪽을 눌렀다가 오른쪽을 떼고 왼쪽을 누르는 경우

    void OnCollisionStay2D(Collision2D other)       //점프상태인지 아닌지를 판단해줌. 이게 있어야 버튼 누를때 언제 점프인지를 안다.
    {
        anim.SetBool("isJumping", false);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        anim.SetBool("isJumping", true);
    }

}