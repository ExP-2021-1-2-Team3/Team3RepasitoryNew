using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryBtn : MonoBehaviour
{
    public GameObject btn;
    void Start()
    {
        btn.gameObject.SetActive(false);
    }

    public void Show()
    {
        btn.gameObject.SetActive(true);
    }
}
