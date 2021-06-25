using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blanket_Carpet : MonoBehaviour
{
    public GameObject touchedObj;
    public GameObject Blanket;
    public GameObject Carpet;
    RaycastHit2D hit;
    public Camera cam;
    public bool isBlanketOpened = false;
    public bool isCarpetOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }   //  레이캐스트로 오브젝트 선택

        if (touchedObj == Blanket)  //  이불 선택 시
        {
            if (isCarpetOpened) //  이전에 카펫을 확인했을 경우
            {
                isBlanketOpened = true;
                cam.transform.position = new Vector3(0, -45, -10);  //  전부 확인된 화면으로 이동
            }
            else // 이전에 카펫을 확인하지 않았을 경우
            {
                isBlanketOpened = true;
                cam.transform.position = new Vector3(0, -15, -10);  //  이불만 확인된 화면으로 이동
            }
            touchedObj = null;
        }
        if (touchedObj == Carpet)   //  카펫 선택시
        {
            if (isBlanketOpened) // 이전에 카펫을 확인했을 경우
            {
                isCarpetOpened = true;
                cam.transform.position = new Vector3(0, -45, -10);  //  전부 확인된 화면으로 이동
            }
            else // 이전에 카펫을 확인하지 않았을 경우
            {
                isCarpetOpened = true;
                cam.transform.position = new Vector3(0, -30, -10);  //  카펫만 확인된 화면으로 이동
            }
            touchedObj = null;
        }
    }
}
