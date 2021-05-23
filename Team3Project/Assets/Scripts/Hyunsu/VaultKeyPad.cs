using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultKeyPad : MonoBehaviour
{
    [SerializeField] public Vault vault;
    public GameObject buttonObj;

    public void OnCliCkCharBtn(string inputChar)    //  ���ĺ� ��ư Ŭ��
    {
        vault.inputString += inputChar; //  ��ü �Էµ� ���ڿ��� ���� �Է��� ���ĺ� �߰�
        vault.numOfChar += 1;
    }
    public void OnClickEraseBtn()   //  ����� ��ư Ŭ��
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