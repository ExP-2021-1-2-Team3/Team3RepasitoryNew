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
    public void OnCliCkCharBtn(string inputChar)    //  ���ĺ� ��ư Ŭ��
    {
        if (vault.numOfChar < 6)    //  �ִ� ���ڼ� 6
        {
            vault.inputString += inputChar; //  ��ü �Էµ� ���ڿ��� ���� �Է��� ���ĺ� �߰�
        }
        else
            return;
    }
    public void OnClickImage(Sprite inputSpr)
    {
        if (vault.numOfChar < 6)
        {
            //���� �̹��� �ֱ�
            apbList[vault.numOfChar].SetActive(true);
            apbList[vault.numOfChar].GetComponent<Image>().sprite = inputSpr;
            vault.numOfChar += 1;   //  �� �Էµ� ���ڼ� + 1
        }
        else
            return;
    }
    public void OnClickEraseBtn()   //  ����� ��ư Ŭ��
    {
        if (vault.numOfChar > 0)
        {
            vault.inputString = vault.inputString.Remove(vault.numOfChar - 1);  //  �� �ڿ� �ִ� ���� ����
            
        }
    }
    public void OnClickEraseImage()
    {
        if (vault.numOfChar > 0)
        {
            apbList[vault.numOfChar - 1].SetActive(false);
            vault.numOfChar -= 1;   // �� �Էµ� ���ڼ� - 1
        }
    }
    public void OnClickCancelBtn()  //  �ݱ� ��ư Ŭ���� Ű�е� �ݱ�
    {
        buttonObj.SetActive(false);
        vault.inputString = ""; //�ݱ� ��ư Ŭ���� �Էµ� ��ȣ ������
        for (int i = 0; i < 6; i++)
        {
            apbList[i].SetActive(false);
        }
        vault.numOfChar = 0;
        CloseBtn.isOpenedUI = false;
    }
}