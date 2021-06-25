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
        passwordText.text = ""; //  입력된 암호 초기화
    }

    void Update()
    {
        passwordText.text = vault.inputString;  //  입력된 암호 주기적으로 업데이트
    }
}
