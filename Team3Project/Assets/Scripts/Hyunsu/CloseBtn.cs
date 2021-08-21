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

    public void OnClickCloseBookBtn()   //  N적힌 책 줌인 화면 닫기
    {
        bookbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseKeyBtn()    // 열쇠 줌인 화면 닫기
    {
        keybuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void onClickKey()    //  열쇠 클릭시 열쇠 획득
    {
        keybuttonObj.SetActive(false);
        keyInven.SetActive(true);
        key.keySound.Play();
        Key.isKeyFound = true;
        isOpenedUI = false;
    }
    public void OnClickClosePosterBtn() //  포스터 줌인 화면 닫기
    {
        posterbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseEBookBtn()  //  E적힌 책 줌인 화면 닫기
    {
        E_BookbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseMonitorBtn()    //   T출력된 모니터 줌인 화면 닫기
    {
        monitorbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickCloseDrawerBtn() //  서랍 줌이 화면 닫기
    {
        drawerbuttonObj.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickKeyHoleBtn()
    {
        if (Key.isKeyFound)   //  열쇠 보유시 열쇠구명 누르면 서랍이 열림
        {
            Key.isKeyUsed = true;
            Drawer.isDrawerOpened = true;
            closedDrawer.SetActive(false);
            openedDrawer.SetActive(true);
        }
    }
    public void OnClickOpenedDrawer()   //  열린 서랍 줌인 화면 출력
    {
        openedDrawerCloseBtn.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickMemo()   //   I메모 줌인 화면 출력
    {
        openedDrawer.SetActive(false);
        memoJoomIn.SetActive(true);
    }
    public void OnClickMemoCloseBtn()   //  I메모 줌인 화면 닫기
    {
        memoJoomIn.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickAlbumCloseBtn()  //  X앨범 줌인 화면 닫기
    {
        albumBtn.SetActive(false);
        isOpenedUI = false;
    }
    public void OnClickClock()  //  알람시계 클릭시 게임 종료
    {
        //상훈코드~
        if (LoadManagerSH.singleTon != null)
        {
            LoadManagerSH.singleTon.GameEnd();
        }
    }
}
