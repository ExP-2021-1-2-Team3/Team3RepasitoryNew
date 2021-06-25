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
    public static bool isDrawerOpened = false;

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
        }   // 레이캐스트로 오브젝트 선택

        if (touchedObj == aimObj)   //  선택된 오브젝트 = 서랍일 경우 서랍 줌인 화면 출력
        {
            if (isDrawerOpened) //  이전에 서랍을 열었을 경우 바로 열린 서랍 화면 출력
            {
                openedDrawer.SetActive(true);
            }
            else // 처음으로 서랍을 열 경우 잠긴 서랍 화면 출력
            {
                drawerJoomin.SetActive(true);
                CloseBtn.isOpenedUI = true;
                touchedObj = null;
            }
            touchedObj = null;
        }
    }
}