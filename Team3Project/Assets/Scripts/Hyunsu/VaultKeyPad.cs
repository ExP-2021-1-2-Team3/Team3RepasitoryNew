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
        vault.numOfChar += 1;   //  �� �Էµ� ���ڼ�
    }
    public void OnClickEraseBtn()   //  ����� ��ư Ŭ��
    {
        if (vault.numOfChar > 0)
        {
            vault.inputString = vault.inputString.Remove(vault.numOfChar - 1);  //  �� �ڿ� �ִ� ���� ����
            vault.numOfChar -= 1;   // �� �Էµ� ���ڼ� - 1
        }
    }
    public void OnClickCancelBtn()  //  �ݱ� ��ư Ŭ���� Ű�е� �ݱ�
    {
        buttonObj.SetActive(false);
        vault.inputString = ""; //�ݱ� ��ư Ŭ���� �Էµ� ��ȣ ������
        CloseBtn.isOpenedUI = false;
    }
}