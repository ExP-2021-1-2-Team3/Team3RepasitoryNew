using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Btnclick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Player player;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator anim;
    [SerializeField] GameManagerJH game;
    [SerializeField] float acceleration = 5f;
    [SerializeField] float Maxvel = 5f;

    public static int LeftBtnClickCounter = 0;
    public static int RightBtnClickCounter = 0;
    public static int JumpBtnClickCounter = 0;
    public static int InteractionBtnClickCounter = 0;
    public void OnPointerDown(PointerEventData ped)
    {
        switch (gameObject.name)
        {            
            case "LeftBtn":
                if (!game.isInRootedCoroutine)
                {
                    player.anim.SetBool("isMoving", true);
                    if (player.horizontalVec < Maxvel)
                    {
                        player.horizontalVec = (-1) * (1f + acceleration * Time.deltaTime);
                    }
                    else
                        player.horizontalVec = (-1) * Maxvel;                             //�ִ� �ӷ��� �ᱹ Maxspeed * speed�̴�.  
                }                   
                else
                    LeftBtnClickCounter++;
                break;
            case "RightBtn":
                if (!game.isInRootedCoroutine)
                {
                    player.anim.SetBool("isMoving", true);
                    if (player.horizontalVec < Maxvel)
                    {
                        player.horizontalVec = 1f + acceleration * Time.deltaTime;
                    }
                    else
                        player.horizontalVec = Maxvel;
                }                    
                else
                    RightBtnClickCounter++;
                break;
            case "JumpBtn":
                if (!game.isInRootedCoroutine)
                {
                    if (anim.GetBool("isJumping") == false)
                    {
                        rigid.AddForce(Vector2.up * player.jumpPower, ForceMode2D.Impulse);
                        anim.SetBool("isJumping", true);
                    }                   
                }                   
                else
                    JumpBtnClickCounter++;
                break;
            case "Interaction":
                if (!game.isInRootedCoroutine)
                {
                    if (game.touchedC1)                                              //��ũ���� 1�� ��� �ִ� ������ ��
                        game.Curled1.SetActive(false);                               //�����.

                    if (game.touchedC2)                                              //��ũ���� 2�� ��� �ִ� ������ ��
                        game.Curled2.SetActive(false);                               //�����.

                    if (game.touchedC3)                                              //��ũ���� 3�� ��� �ִ� ������ ��
                        game.Curled3.SetActive(false);                               //�����.

                    if (game.touchedC4)                                              //��ũ���� 4�� ��� �ִ� ������ ��
                        game.Curled4.SetActive(false);                               //�����.

                    if (game.touchedDoor)                                            //��ũ���� 1, 2�� ��ȣ�ۿ� ������ �� -> 2�� ���� ����ִ� �����̸�
                        player.transform.position = game.firstFloorPosition;         //�÷��̾��� ��ġ�� 1�� ���� �ִ� ������ �ű��.          
                }
                else
                    InteractionBtnClickCounter++;
                break;
                
                default: break;
        }
      
    }
 

    public void OnPointerUp(PointerEventData ped)
    {
        if(gameObject.name=="RightBtn"|| gameObject.name == "LeftBtn")
        {
            player.horizontalVec = 0f;
            player.anim.SetBool("isMoving", false);
        }
               
    }

}





