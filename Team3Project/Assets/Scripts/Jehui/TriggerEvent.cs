using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] GameObject realObject;
    [SerializeField] GameManagerJH game;
    //게임 내에서 
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (realObject.name == "Lever1")
            game.touchedLever1 = true;
        
        if (realObject.name == "Lever2")        
            game.touchedLever2 = true;
        
        if (realObject.name == "Lever3")       
            game.touchedLever3 = true;
        
        if (realObject.name == "Lever4")        
            game.touchedLever4 = true;

        /*if (realObject.name == "testLever")
        {
            game.touchedTestLever = true;
            Debug.Log("touchedTestLever true");
        }*/
            

        if (realObject.name == "DoorIN")
            if (!game.lever1.activeSelf && !game.lever2.activeSelf)
            {
                game.isDoorActive = true;
                game.touchedDoor = true;
                Debug.Log("touchedDoor true");
            }
                

        if (realObject.name == "Alarm")
        {
            game.isGameClear = true;
            game.gameClear();
        }            
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (realObject.name == "DoorIN")
            if (!game.lever1.activeSelf && !game.lever2.activeSelf)
                game.touchedDoor = false;

        if (realObject.name == "Lever1")
            game.touchedLever1 = false;

        if (realObject.name == "Lever2")
            game.touchedLever2 = false;

        if (realObject.name == "Lever3")
            game.touchedLever3 = false;

        if (realObject.name == "Lever4")
            game.touchedLever4 = false;

        /*if (realObject.name == "testLever")
            game.touchedTestLever = false;*/
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (realObject.tag == "Thorns")
        {
            Debug.Log(realObject.name + "에 닿아서 리스폰되었습니다.");
            game.respawn();
        }        
            
    }
}
