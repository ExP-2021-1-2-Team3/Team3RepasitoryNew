using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public Camera cam;
    public GameObject touchedObject;
    RaycastHit2D hit;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
            {
                if (hit.collider.gameObject != null)
                    touchedObject = hit.collider.gameObject;
                if (touchedObject.CompareTag("Obs"))
                {
                    touchedObject.SetActive(false);
                }
            }

        }

    }
}
