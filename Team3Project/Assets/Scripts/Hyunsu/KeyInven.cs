using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInven : MonoBehaviour
{
    [SerializeField] GameObject keyinven;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Key.isKeyFound)
        {
            if (Key.isKeyUsed)
            {
                keyinven.SetActive(false);
                Key.isKeyFound = false;
            }
        }
    }
}
