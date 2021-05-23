using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInInven : MonoBehaviour
{
    public static bool isKeyUsed = false;
    public GameObject keyInven;
    // Start is called before the first frame update
    void Start()
    {
        keyInven.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Key.isKeyFound)
        {
            keyInven.SetActive(true);
        }
        if (isKeyUsed)
        {
            keyInven.SetActive(false);
        }
    }
}
