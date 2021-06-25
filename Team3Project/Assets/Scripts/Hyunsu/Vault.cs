using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vault : MonoBehaviour
{
    public GameObject keypad;
    public Camera cam;
    public GameObject openedVault;

    string password;            //  ��ȣ
    public string inputString;  //  ���� �Էµ� ��ȣ
    public int numOfChar;       //  �Էµ� ��ȣ�� ���ĺ� ��
    public bool isVaultOpen;    //  �ݰ� ���ȴ°�?

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
        if (password == inputString)   //  �Էµ� ��ȣ�� ������ ��ȣ�� ���� ��
        {
            keypad.SetActive(false);
            CloseBtn.isOpenedUI = true;
            inputString = "";
            isVaultOpen = true;
            //ȭ�� ���
            openedVault.SetActive(true);
        }
    }
}
