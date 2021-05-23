using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordInput : MonoBehaviour
{
    [SerializeField] public Vault vault;
    public Text passwordText;

    void Start()
    {
        passwordText.text = ""; //  ��ȣ �ؽ�Ʈ �ʱ�ȭ
    }

    void Update()
    {
        passwordText.text = vault.inputString;  //  �Էµ� ��ȣ ���
    }
}
