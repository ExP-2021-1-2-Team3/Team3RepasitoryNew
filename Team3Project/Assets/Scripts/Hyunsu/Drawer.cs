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
        }   // ����ĳ��Ʈ�� ������Ʈ ����

        if (touchedObj == aimObj)   //  ���õ� ������Ʈ = ������ ��� ���� ���� ȭ�� ���
        {
            if (isDrawerOpened) //  ������ ������ ������ ��� �ٷ� ���� ���� ȭ�� ���
            {
                openedDrawer.SetActive(true);
            }
            else // ó������ ������ �� ��� ��� ���� ȭ�� ���
            {
                drawerJoomin.SetActive(true);
                CloseBtn.isOpenedUI = true;
                touchedObj = null;
            }
            touchedObj = null;
        }
    }
}