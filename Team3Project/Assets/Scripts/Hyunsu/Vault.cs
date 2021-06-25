using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vault : MonoBehaviour
{
    public GameObject keypad;
    public Camera cam;
    public GameObject openedVault;

    string password;            //  암호
    public string inputString;  //  현재 입력된 암호
    public int numOfChar;       //  입력된 암호의 알파벳 수
    public bool isVaultOpen;    //  금고가 열렸는가?

    void Start()
    {
        openedVault.SetActive(false);
        password = "NOEXIT";
        inputString = "";
        isVaultOpen = false;
        numOfChar = 0;
    }

    void Update()
    {
        if (password == inputString)   //  입력된 암호와 설정된 암호가 같을 시
        {
            keypad.SetActive(false);
            CloseBtn.isOpenedUI = true;
            inputString = "";
            isVaultOpen = true;
            //화면 출력
            openedVault.SetActive(true);
        }
    }
}
