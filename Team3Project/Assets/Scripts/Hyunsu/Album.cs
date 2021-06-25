using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Album : MonoBehaviour
{
    public GameObject touchedObj;
    public GameObject aimObj;
    public GameObject albumJoomin;
    RaycastHit2D hit;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        albumJoomin.SetActive(false);
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

        if (touchedObj == aimObj)   //  선택된 오브젝트 = 앨범 일 경우 화면에 앨범 줌인 출력
        {
            albumJoomin.SetActive(true);
            CloseBtn.isOpenedUI = true;
            touchedObj = null;
        }
    }
}
