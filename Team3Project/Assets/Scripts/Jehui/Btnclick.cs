using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Btnclick : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator anim;
    [SerializeField] GameManagerJH game;
    [SerializeField] SoundManagerJH sound;


    public static int LeftBtnClickCounter = 0;
    public static int RightBtnClickCounter = 0;
    public static int JumpBtnClickCounter = 0;
    public static int InteractionBtnClickCounter = 0;

    public static bool isRightBtnDown = false;
    public static bool isLeftBtnDown = false;

    public bool canPlayIntSound = false;

    /*
     * �̺�Ʈ Ʈ���Ÿ� �̿��� �� �ֵ��� �ڵ带 © ���̴�.
     * 
     * 1. Select : ���������� ���� ����� �̺�Ʈ.
     *      �� ��� isBtnClicked�� true�� �Ǿ�� �Ѵ�.(��� ����, left, right���� �ش�.)      
     *      
     * 2. OnPointerDown: �ѹ� �� ������ ����� �̺�Ʈ.
     *      �� ��� isInRootedCoroutine�� �� ī���Ͱ� �ö󰡾� �Ѵ�.(��� ��ư�� �� �ش�.)
     *      
     *      
     * 3. OnPointerUp: ���� ���¿��� ���� ����� �̺�Ʈ.
     *      �� ��� isBtnClicked�� false�� �Ǿ�� �Ѵ�.(��� ����)
     */

    public void forVelocity()
    {
        if(!game.isInRootedCoroutine) //rootedtime�� ���� �ƴ� ��
        {
            switch (gameObject.name)
            {
                case "LeftBtn":
                    isLeftBtnDown = true;
                    break;
                case "RightBtn":
                    isRightBtnDown = true;
                    break;
            }
        }
    }
    public void forclick()
    {
        switch (gameObject.name)
        {
            case "LeftBtn":
                if (game.isInRootedCoroutine)
                {
                    LeftBtnClickCounter++;
                    Debug.Log("���� Ŭ�� ��: " + LeftBtnClickCounter);
                }
                break;

            case "RightBtn":
                if (game.isInRootedCoroutine)
                {
                    RightBtnClickCounter++;
                    Debug.Log("������ Ŭ�� ��: " + RightBtnClickCounter);
                }                   
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
                {                
                    JumpBtnClickCounter++;
                    Debug.Log("���� Ŭ�� ��: " + JumpBtnClickCounter);
                }                  
                break;

            case "Interaction":
                if (!game.isInRootedCoroutine)
                {
                    if (game.touchedC1)
                    {                                                                                     
                        game.Curled1.SetActive(false);                               
                        game.Curled1r.SetActive(true);
                        canPlayIntSound = true;
                    }
                    if (game.touchedC2)
                    {
                        game.Curled2.SetActive(false);
                        game.Curled2r.SetActive(true);
                        canPlayIntSound = true;
                    }    
                    if (game.touchedC3)
                    {
                        game.Curled3.SetActive(false);
                        game.Curled3r.SetActive(true);
                        canPlayIntSound = true;
                    }                                                                     
                    if (game.touchedC4)
                    {
                        game.Curled4.SetActive(false);
                        game.Curled4r.SetActive(true);
                        canPlayIntSound = true;
                    }
                    if (game.touchedDoor)
                    {                                                                //��ũ���� 1, 2�� ��ȣ�ۿ� ������ �� -> 2�� ���� ����ִ� �����̸�
                        player.transform.position = game.firstFloorPosition;         //�÷��̾��� ��ġ�� 1�� ���� �ִ� ������ �ű��.
                        canPlayIntSound = true;
                    }
                }
                else
                {
                    InteractionBtnClickCounter++;
                    Debug.Log("���ͷ��� Ŭ�� ��: " + InteractionBtnClickCounter);
                }
                break;

            default: break;
        }

    }


    public void OnPointerUp()
    {       
        switch (gameObject.name)
        {
            case "RightBtn":
                isRightBtnDown = false;
                Debug.Log("isrightbtndown �ο� �� false��");
                break;
            case "LeftBtn":
                isLeftBtnDown = false;
                Debug.Log("isleftbtndown �ο� �� false��");
                break;
            default: break;
        }
    }

}