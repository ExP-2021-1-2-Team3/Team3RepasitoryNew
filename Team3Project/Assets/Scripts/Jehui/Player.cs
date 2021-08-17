using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;               //플레이어의 애니메이션 관장
    public SpriteRenderer sRenderer;    //플레이어의 스프라이트렌더러 관장
    public Rigidbody2D rigid;           //플레이어 강체화
    public GameManagerJH game;
    public AudioSource audioWalk;

    public bool keyLeftOn;
    public bool keyRightOn;
    public bool keyJumpOn;

    public float jumpPower;             //플레이어 점프시 가할 힘 설정 필요
    public float horizontalVec;         //필요한건 수평방향 벡터만 설정해주면 됨.
    public float acceleration = 1.5f;
    public float Maxvel = 15f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();                                        //player의 움직임 표현해야 함.
        sRenderer = GetComponent<SpriteRenderer>();                                 //player에서 이동방향에 따른 좌우반전시켜줌.
        anim = GetComponent<Animator>();                                            //player의 state에 따른 animation 조정.
        audioWalk = GetComponent<AudioSource>();
    }

    void Update()
    {
        rigid.velocity = new Vector2(horizontalVec, rigid.velocity.y);

        if(Input.GetKey(KeyCode.RightArrow))
        {
            if (!game.isInRootedCoroutine)
            {
                keyRightOn = true;
                Debug.Log("오른쪽 버튼 눌림");
                audioWalk.Play();
            }
            if (anim.GetBool("isJumping"))
                audioWalk.Stop();
        }
        else
        {
            keyRightOn = false;
            audioWalk.Stop();
        }
            

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!game.isInRootedCoroutine)
            {
                keyLeftOn = true;
                Debug.Log("왼쪽 버튼 눌림");
                audioWalk.Play();
            }              
            if (anim.GetBool("isJumping"))
                audioWalk.Stop();
        }
        else
        {
            keyLeftOn = false;
            audioWalk.Stop();
        }


        //플레이어 방향전환
        if (rigid.velocity.x < 0)
            sRenderer.flipX = true;
        else if (rigid.velocity.x > 0)
            sRenderer.flipX = false;

        if (Btnclick.isRightBtnDown || keyRightOn)
        {
            anim.SetBool("isMoving", true);
            if (horizontalVec < Maxvel)
            {
                horizontalVec += acceleration * Time.deltaTime;
            }
            else
                horizontalVec = Maxvel;
        }


        if (Btnclick.isLeftBtnDown || keyLeftOn)
        {
            anim.SetBool("isMoving", true);
            if (horizontalVec > (-1) * Maxvel)
            {
                horizontalVec -= acceleration * Time.deltaTime;
            }
            else
                horizontalVec = (-1) * Maxvel;                             //최대 속력은 결국 Maxspeed * speed이다. 
        }
        
        if(!Btnclick.isLeftBtnDown && !Btnclick.isRightBtnDown && !keyRightOn && !keyLeftOn)
        {
            horizontalVec = 0f;
            anim.SetBool("isMoving", false);
        }
 
        
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        /*if (rigid)
        {
            if (Input.GetButtonUp("Horizontal"))
            {
                //버튼 뗐을 때
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }
        }*/

        //방향전환
        if (Input.GetButtonDown("Horizontal"))
            sRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
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