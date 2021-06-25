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
        vault.numOfChar += 1;   //  총 입력된 글자수
    }
    public void OnClickEraseBtn()   //  지우기 버튼 클릭
    {
        if (vault.numOfChar > 0)
        {
            vault.inputString = vault.inputString.Remove(vault.numOfChar - 1);  //  맨 뒤에 있는 문자 삭제
            vault.numOfChar -= 1;   // 총 입력된 글자수 - 1
        }
    }
    public void OnClickCancelBtn()  //  닫기 버튼 클릭시 키패드 닫기
    {
        buttonObj.SetActive(false);
        vault.inputString = ""; //닫기 버튼 클릭시 입력된 암호 지워짐
        CloseBtn.isOpenedUI = false;
    }
}