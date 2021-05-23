using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    public GameObject touchedObj;
    public GameObject aimObj;
    public GameObject monitorJoomin;
    RaycastHit2D hit;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        monitorJoomin.SetActive(false);
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
            monitorJoomin.SetActive(true);
            CloseBtn.isOpenedUI = true;
            Debug.Log("T가 적힌 모니터 발견!");
            touchedObj = null;
        }
    }
}