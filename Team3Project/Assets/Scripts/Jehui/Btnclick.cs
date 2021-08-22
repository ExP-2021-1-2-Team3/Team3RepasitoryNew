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
     * 이벤트 트리거를 이용할 수 있도록 코드를 짤 것이다.
     * 
     * 1. Select : 지속적으로 누를 경우의 이벤트.
     *      이 경우 isBtnClicked가 true가 되어야 한다.(운동을 관리, left, right에만 해당.)      
     *      
     * 2. OnPointerDown: 한번 딱 눌렀을 경우의 이벤트.
     *      이 경우 isInRootedCoroutine일 때 카운터가 올라가야 한다.(모든 버튼에 다 해당.)
     *      
     *      
     * 3. OnPointerUp: 누른 상태에서 뗐을 경우의 이벤트.
     *      이 경우 isBtnClicked가 false가 되어야 한다.(운동을 관리)
     */

    public void forVelocity()
    {
        if(!game.isInRootedCoroutine) //rootedtime이 아직 아닐 때
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
                    Debug.Log("왼쪽 클릭 수: " + LeftBtnClickCounter);
                    soundClick.PlayOneShot(clipClick);
                }
                break;

            case "RightBtn":
                if (game.isInRootedCoroutine)
                {
                    RightBtnClickCounter++;
                    Debug.Log("오른쪽 클릭 수: " + RightBtnClickCounter);
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
                    Debug.Log("점프 클릭 수: " + JumpBtnClickCounter);
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
                    {                                                                //웅크린자 1, 2와 상호작용 성공한 후 -> 2층 문에 닿아있는 상태이면
                        player.transform.position = game.firstFloorPosition;         //플레이어의 위치를 1층 문이 있는 곳으로 옮긴다.
                        game.didTeleported = true;
                        canPlayIntSound = true;
                    }
                }
                else
                {
                    InteractionBtnClickCounter++;
                    Debug.Log("인터랙션 클릭 수: " + InteractionBtnClickCounter);
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
                Debug.Log("isrightbtndown 부울 값 false됨");
                break;
            case "LeftBtn":
                isLeftBtnDown = false;
                Debug.Log("isleftbtndown 부울 값 false됨");
                break;
            default: break;
        }
    }

}