using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;               //�÷��̾��� �ִϸ��̼� ����
    public SpriteRenderer sRenderer;    //�÷��̾��� ��������Ʈ������ ����
    public Rigidbody2D rigid;           //�÷��̾� ��üȭ
    public GameManagerJH game;
    public AudioSource audioWalk;

    public bool keyLeftOn;
    public bool keyRightOn;
    public bool keyJumpOn;

    public float jumpPower;             //�÷��̾� ������ ���� �� ���� �ʿ�
    public float horizontalVec;         //�ʿ��Ѱ� ������� ���͸� �������ָ� ��.
    public float acceleration = 1.5f;
    public float Maxvel = 15f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();                                        //player�� ������ ǥ���ؾ� ��.
        sRenderer = GetComponent<SpriteRenderer>();                                 //player���� �̵����⿡ ���� �¿����������.
        anim = GetComponent<Animator>();                                            //player�� state�� ���� animation ����.
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
                Debug.Log("������ ��ư ����");
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
                Debug.Log("���� ��ư ����");
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


        //�÷��̾� ������ȯ
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
                horizontalVec = (-1) * Maxvel;                             //�ִ� �ӷ��� �ᱹ Maxspeed * speed�̴�. 
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
                //��ư ���� ��
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }
        }*/

        //������ȯ
        if (Input.GetButtonDown("Horizontal"))
            sRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
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