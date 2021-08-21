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

    public GameObject keyInven;
    public Key key;

    public static bool isOpenedUI = false;

    public void OnClickCloseBookBtn()   //  N���� å ���� ȭ�� �ݱ�
    {
        bookbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseKeyBtn()    // ���� ���� ȭ�� �ݱ�
    {
        keybuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void onClickKey()    //  ���� Ŭ���� ���� ȹ��
    {
        keybuttonObj.SetActive(false);
        keyInven.SetActive(true);
        key.keySound.Play();
        Key.isKeyFound = true;
        isOpenedUI = false;
    }
    public void OnClickClosePosterBtn() //  ������ ���� ȭ�� �ݱ�
    {
        posterbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseEBookBtn()  //  E���� å ���� ȭ�� �ݱ�
    {
        E_BookbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseMonitorBtn()    //   T��µ� ����� ���� ȭ�� �ݱ�
    {
        monitorbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseDrawerBtn() //  ���� ���� ȭ�� �ݱ�
    {
        drawerbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickKeyHoleBtn()
    {
        if (Key.isKeyFound)   //  ���� ������ ���豸�� ������ ������ ����
        {
            Key.isKeyUsed = true;
            Drawer.isDrawerOpened = true;
            closedDrawer.SetActive(false);
            openedDrawer.SetActive(true);
        }
    }
    public void OnClickOpenedDrawer()   //  ���� ���� ���� ȭ�� ���
    {
        openedDrawerCloseBtn.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickMemo()   //   I�޸� ���� ȭ�� ���
    {
        openedDrawer.SetActive(false);
        memoJoomIn.SetActive(true);
    }
    public void OnClickMemoCloseBtn()   //  I�޸� ���� ȭ�� �ݱ�
    {
        memoJoomIn.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickAlbumCloseBtn()  //  X�ٹ� ���� ȭ�� �ݱ�
    {
        albumBtn.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickClock()  //  �˶��ð� Ŭ���� ���� ����
    {
        //�����ڵ�~
        if (LoadManagerSH.singleTon != null)
        {
            LoadManagerSH.singleTon.GameEnd();
        }
    }
}
