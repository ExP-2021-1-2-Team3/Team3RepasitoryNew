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
    [SerializeField] AudioSource computerSound;

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
        }   //  레이캐스트로 오브젝트 선택

        if (touchedObj == aimObj)   // 선택된 오브젝트 = 모니터 일 경우 모니터 줌인 화면 출력
        {
            computerSound.Play();
            monitorJoomin.SetActive(true);
            CloseBtn.isOpenedUI = true;
            touchedObj = null;
        }
    }
}