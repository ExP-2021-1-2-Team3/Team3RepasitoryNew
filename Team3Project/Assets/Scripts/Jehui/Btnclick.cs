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
                        player.horizontalVec = (-1) * Maxvel;                             //최대 속력은 결국 Maxspeed * speed이다.  
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
                    if (game.touchedC1)                                              //웅크린자 1에 닿아 있는 상태일 때
                        game.Curled1.SetActive(false);                               //지운다.

                    if (game.touchedC2)                                              //웅크린자 2에 닿아 있는 상태일 때
                        game.Curled2.SetActive(false);                               //지운다.

                    if (game.touchedC3)                                              //웅크린자 3에 닿아 있는 상태일 때
                        game.Curled3.SetActive(false);                               //지운다.

                    if (game.touchedC4)                                              //웅크린자 4에 닿아 있는 상태일 때
                        game.Curled4.SetActive(false);                               //지운다.

                    if (game.touchedDoor)                                            //웅크린자 1, 2와 상호작용 성공한 후 -> 2층 문에 닿아있는 상태이면
                        player.transform.position = game.firstFloorPosition;         //플레이어의 위치를 1층 문이 있는 곳으로 옮긴다.          
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





