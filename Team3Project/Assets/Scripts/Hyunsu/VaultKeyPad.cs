using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultKeyPad : MonoBehaviour
{
    [SerializeField] public Vault vault;
    public GameObject buttonObj;

    public void OnCliCkCharBtn(string inputChar)    //  알파벳 버튼 클릭
    {
        vault.inputString += inputChar; //  전체 입력된 문자열에 현재 입력한 알파벳 추가
        vault.numOfChar += 1;
    }
    public void OnClickEraseBtn()   //  지우기 버튼 클릭
    {
        if (vault.numOfChar > 0)
        {
            vault.inputString = vault.inputString.Remove(vault.numOfChar - 1);
            vault.numOfChar -= 1;
        }
    }
    public void OnClickCancelBtn()
    {
        buttonObj.SetActive(false);
        CloseBtn.isOpenedUI = false;
    }
}