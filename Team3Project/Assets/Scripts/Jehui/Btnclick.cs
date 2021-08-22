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
    [SerializeField] AudioSource soundClick;
    public AudioClip clipClick;
    public AudioClip clipInt;

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
                    soundClick.PlayOneShot(clipClick);
                }
                break;

            case "RightBtn":
                if (game.isInRootedCoroutine)
                {
                    RightBtnClickCounter++;
                    Debug.Log("������ Ŭ�� ��: " + RightBtnClickCounter);
                    soundClick.PlayOneShot(clipClick);
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
                    soundClick.PlayOneShot(clipClick);
                }                  
                break;

            case "Interaction":
                if (!game.isInRootedCoroutine)
                {
                    if (game.touchedLever1)
                    {
                        game.lever1.SetActive(false);
                        game.lever1r.SetActive(true);
                        StartCoroutine(game.RootedTimer());
                        soundClick.PlayOneShot(clipInt);
                    }
                    if (game.touchedLever2)
                    {
                        game.lever2.SetActive(false);
                        game.lever2r.SetActive(true);
                        StartCoroutine(game.RootedTimer());
                        soundClick.PlayOneShot(clipInt);
                    }    
                    if (game.touchedLever3)
                    {
                        game.lever3.SetActive(false);
                        game.lever3r.SetActive(true);
                        StartCoroutine(game.RootedTimer());
                        soundClick.PlayOneShot(clipInt);
                    }                                                                     
                    if (game.touchedLever4)
                    {                        
                        game.lever4.SetActive(false);
                        game.lever4r.SetActive(true);
                        StartCoroutine(game.RootedTimer());
                        soundClick.PlayOneShot(clipInt);
                    }
                    /*if (game.touchedTestLever)
                    {
                        game.testLever.SetActive(false);
                        game.testLeverr.SetActive(true);
                        StartCoroutine(game.RootedTimer());
                    }*/
                    if (game.touchedDoor)
                    {                                                                //��ũ���� 1, 2�� ��ȣ�ۿ� ������ �� -> 2�� ���� ����ִ� �����̸�
                        player.transform.position = game.firstFloorPosition;         //�÷��̾��� ��ġ�� 1�� ���� �ִ� ������ �ű��.
                        game.didTeleported = true;
                        canPlayIntSound = true;
                    }
                }
                else
                {
                    InteractionBtnClickCounter++;
                    Debug.Log("���ͷ��� Ŭ�� ��: " + InteractionBtnClickCounter);
                    soundClick.PlayOneShot(clipClick);
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