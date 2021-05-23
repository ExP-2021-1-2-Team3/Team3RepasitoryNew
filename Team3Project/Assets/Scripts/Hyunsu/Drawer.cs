using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public GameObject touchedObj;
    public GameObject aimObj;
    public GameObject drawerJoomin;
    public GameObject openedDrawer;
    RaycastHit2D hit;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        drawerJoomin.SetActive(false);
        openedDrawer.SetActive(false);
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
            if (KeyInInven.isKeyUsed)
            {
                openedDrawer.SetActive(true);
            }
            else
            {
                drawerJoomin.SetActive(true);
                CloseBtn.isOpenedUI = true;
                Debug.Log("잠겨있는 서랍 발견! 열쇠가 필요할거 같은데...");
                touchedObj = null;
            }
            touchedObj = null;
        }
    }
}