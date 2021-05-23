using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerKey : MonoBehaviour
{
    public bool isKeyFoundDrawer = false;
    public bool isDrawerOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Key.isKeyFound)
        {
            isKeyFoundDrawer = true;
            isDrawerOpened = true;
        }
    }
}
