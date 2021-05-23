using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenBtn : MonoBehaviour
{
    public GameObject touchedObj;
    public GameObject aimObj;
    public GameObject invenZoomin;
    RaycastHit2D hit;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        invenZoomin.SetActive(false);
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
            invenZoomin.SetActive(true);
            CloseBtn.isOpenedUI = true;
            touchedObj = null;
            Debug.Log("인벤토리 오픈");
        }
    }
}
