using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] GameObject realObject;
    [SerializeField] GameManagerJH game;
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (realObject.name == "CurledupPlayer1")
            game.touchedC1 = true;
        
        if (realObject.name == "CurledupPlayer2")        
            game.touchedC2 = true;
        
        if (realObject.name == "CurledupPlayer3")       
            game.touchedC3 = true;
        
        if (realObject.name == "CurledupPlayer4")        
            game.touchedC4 = true;
        
        if (!game.Curled1.activeSelf && !game.Curled2.activeSelf)
            if (realObject.name == "doorIN")
                game.touchedDoor = true;

        if (realObject.name == "Alarm")
        {
            game.isGameClear = true;
            game.gameClear();
        }
            
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (realObject.tag == "Thorns")        
            game.respawn();        
    }
}
