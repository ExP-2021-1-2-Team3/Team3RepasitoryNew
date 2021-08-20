using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public bool playonce;
    void Start() 
    {
        playonce = false;
    }
    void Update()
    {
        if ((Score.ItemAmount == 4) && (playonce == false))
        {
            LoadManagerSH.singleTon.GameEnd();
            ItemSound.FinishSound();
            playonce = true;
        }
    }
    
}
