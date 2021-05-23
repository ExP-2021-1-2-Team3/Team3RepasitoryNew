using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject touchedObj;
    public GameObject aimObj;
    public GameObject keyJoomin;
    public GameObject keySprite;
    RaycastHit2D hit;
    public Camera cam;
    public static bool isKeyFound = false;

    // Start is called before the first frame update
    void Start()
    {
        keyJoomin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CloseBtn.isOpenedUI)
            {
                return;
            }
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                touchedObj = hit.collider.gameObject;
            }
        }

        if (touchedObj == aimObj)
        {
            keyJoomin.SetActive(true);
            CloseBtn.isOpenedUI = true;
            touchedObj = null;
            Debug.Log("¿­¼è ¹ß°ß!");
            isKeyFound = true;
            
        }
        if (isKeyFound)
        {
            keySprite.SetActive(false);
        }
    }
}
