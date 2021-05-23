using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour
{
    [SerializeField] public DrawerKey drawerKey;

    public GameObject closedDrawer;
    public GameObject openedDrawer;

    public GameObject bookbuttonObj;
    public GameObject keybuttonObj;
    public GameObject posterbuttonObj;
    public GameObject E_BookbuttonObj;
    public GameObject monitorbuttonObj;
    public GameObject drawerbuttonObj;
    public GameObject keyHoleBtn;
    public GameObject openedDrawerCloseBtn;
    public GameObject memoBtn;
    public GameObject memoJoomIn;
    public GameObject albumBtn;
    public GameObject InvenClose;
    public static bool isOpenedUI = false;

    public void OnClickCloseBookBtn()
    {
        bookbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseKeyBtn()
    {
        keybuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickClosePosterBtn()
    {
        posterbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseEBookBtn()
    {
        E_BookbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseMonitorBtn()
    {
        monitorbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseDrawerBtn()
    {
        drawerbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickKeyHoleBtn()
    {
        if (drawerKey.isDrawerOpened)
        {
            Debug.Log("������ ���ȴ�!");
            closedDrawer.SetActive(false);
            openedDrawer.SetActive(true);
            KeyInInven.isKeyUsed = true;
        }
        else
        {
            Debug.Log("���� ���踦 ���� ���ߴ�...");
        }
    }
    public void OnClickOpenedDrawer()
    {
        openedDrawerCloseBtn.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickMemo()
    {
        Debug.Log("I�� ���� �޸� �߰�!");
        openedDrawer.SetActive(false);
        memoJoomIn.SetActive(true);
    }
    public void OnClickMemoCloseBtn()
    {
        memoJoomIn.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickAlbumCloseBtn()
    {
        albumBtn.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickClock()
    {
        Debug.Log("�ῡ�� ����ϴ�...");
    }
    public void OnClickInvenClose()
    {
        InvenClose.SetActive(false);
        isOpenedUI = false;
    }
}
