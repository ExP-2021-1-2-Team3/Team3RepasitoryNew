using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;               //�÷��̾��� �ִϸ��̼� ����
    public SpriteRenderer sRenderer;    //�÷��̾��� ��������Ʈ������ ����
    public Rigidbody2D rigid;           //�÷��̾� ��üȭ
    public BgSoundManagerJH sound;
    public float jumpPower;             //�÷��̾� ������ ���� �� ���� �ʿ�
    public float horizontalVec;         //�ʿ��Ѱ� ������� ���͸� �������ָ� ��.
    public float acceleration = 1.5f;
    public float Maxvel = 15f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();                                        //player�� ������ ǥ���ؾ� ��.
        sRenderer = GetComponent<SpriteRenderer>();                                 //player���� �̵����⿡ ���� �¿����������.
        anim = GetComponent<Animator>();                                            //player�� state�� ���� animation ����.
    }

    void Update()
    {
        rigid.velocity = new Vector2(horizontalVec, rigid.velocity.y);

        //�÷��̾� ������ȯ
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
                horizontalVec = (-1) * Maxvel;                             //�ִ� �ӷ��� �ᱹ Maxspeed * speed�̴�. 
        }
        
        if(!Btnclick.isLeftBtnDown && !Btnclick.isRightBtnDown)
        {
            horizontalVec = 0f;
            anim.SetBool("isMoving", false);
        }

    }
    // �������� �����ٰ� �������� ���� ������ ������ ���

    void OnCollisionStay2D(Collision2D other)       //������������ �ƴ����� �Ǵ�����. �̰� �־�� ��ư ������ ���� ���������� �ȴ�.
    {
        anim.SetBool("isJumping", false);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        anim.SetBool("isJumping", true);
    }

}