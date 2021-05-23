using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject touchedObj;
    public GameObject aimObj;
    RaycastHit2D hit;
    public Camera cam;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                touchedObj = hit.collider.gameObject;
            }
        }

        if (touchedObj == aimObj)
        {
            Debug.Log("²Þ¿¡¼­ ±ú¾î³³´Ï´Ù.");
            touchedObj = null;
        }
    }
}