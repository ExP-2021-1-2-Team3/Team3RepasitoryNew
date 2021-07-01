using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VaultKeyPad : MonoBehaviour
{
    [SerializeField] public Vault vault;
    public GameObject buttonObj;

    [SerializeField] List<GameObject> apbList = new List<GameObject>();

    private void Start()
    {
        for (int i  = 0; i < 6; i++)
        {
            apbList[i].SetActive(false);
        }
    }
    public void OnCliCkCharBtn(string inputChar)    //  알파벳 버튼 클릭
    {
        if (vault.numOfChar < 6)    //  최대 글자수 6
        {
            vault.inputString += inputChar; //  전체 입력된 문자열에 현재 입력한 알파벳 추가
        }
        else
            return;
    }
    public void OnClickImage(Sprite inputSpr)
    {
        if (vault.numOfChar < 6)
        {
            //글자 이미지 넣기
            apbList[vault.numOfChar].SetActive(true);
            apbList[vault.numOfChar].GetComponent<Image>().sprite = inputSpr;
            vault.numOfChar += 1;   //  총 입력된 글자수 + 1
        }
        else
            return;
    }
    public void OnClickEraseBtn()   //  지우기 버튼 클릭
    {
        if (vault.numOfChar > 0)
        {
            vault.inputString = vault.inputString.Remove(vault.numOfChar - 1);  //  맨 뒤에 있는 문자 삭제
            
        }
    }
    public void OnClickEraseImage()
    {
        if (vault.numOfChar > 0)
        {
            apbList[vault.numOfChar - 1].SetActive(false);
            vault.numOfChar -= 1;   // 총 입력된 글자수 - 1
        }
    }
    public void OnClickCancelBtn()  //  닫기 버튼 클릭시 키패드 닫기
    {
        buttonObj.SetActive(false);
        vault.inputString = ""; //닫기 버튼 클릭시 입력된 암호 지워짐
        for (int i = 0; i < 6; i++)
        {
            apbList[i].SetActive(false);
        }
        vault.numOfChar = 0;
        CloseBtn.isOpenedUI = false;
    }
}