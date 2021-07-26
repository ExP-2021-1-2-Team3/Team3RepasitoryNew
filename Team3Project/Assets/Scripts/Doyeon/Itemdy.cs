using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemdy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Score.ItemAmount += 1;
        ItemSound.ItemdySound();
        Destroy(gameObject);
        //Instantiate(gameObject, new Vector3(x,y,z), Quaternion.identity);
    }

}
