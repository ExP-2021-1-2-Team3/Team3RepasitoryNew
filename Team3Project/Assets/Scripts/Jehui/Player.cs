using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer sRenderer;
    public Rigidbody2D rigid;
    public float jumpPower; //������
    public float horizontalVec;


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
    }
    void OnCollisionStay2D(Collision2D other)
    {
        anim.SetBool("isJumping", false);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        anim.SetBool("isJumping", true);
    }

}