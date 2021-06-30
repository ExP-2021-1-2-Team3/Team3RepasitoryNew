using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public static int hp = 3;

    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject life1b;
    public GameObject life2b;
    public GameObject life3b;

    void Start()
    {
        life1.GetComponent<Image>().enabled = true;
        life1b.GetComponent<Image>().enabled = false;

        life2.GetComponent<Image>().enabled = true;
        life2b.GetComponent<Image>().enabled = false;

        life3.GetComponent<Image>().enabled = true;
        life3b.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        switch (hp)
        {
            case 2:
                life3.GetComponent<Image>().enabled = false;
                life3b.GetComponent<Image>().enabled = true;
                break;
            case 1:
                life2.GetComponent<Image>().enabled = false;
                life2b.GetComponent<Image>().enabled = true;
                break;
            case 0:
                life1.GetComponent<Image>().enabled = false;
                life1b.GetComponent<Image>().enabled = true;
                //game over. 
                break;
        }
    }
}
