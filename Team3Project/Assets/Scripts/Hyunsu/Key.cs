using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject touchedObj;
    public GameObject aimObj;
    public GameObject keyJoomin;
    public GameObject keySprite;
    RaycastHit2D hit;
    public Camera cam;
    public static bool isKeyFound = false;
    public static bool isKeyUsed = false;
    [SerializeField] public AudioSource keySound;

    // Start is called before the first frame update
    void Start()
    {
        keyJoomin.SetActive(false);
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
        } // 레이캐스트로 오브젝트 선택

        if (touchedObj == aimObj)   //  선택된 오브젝트 = 열쇠 일 경우 열쇠 줌인 화면 출력
        {
            keyJoomin.SetActive(true);
            CloseBtn.isOpenedUI = true;
            touchedObj = null;
        }
        if (isKeyFound) //  열쇠를 찾았을 경우 열쇠 스프라이트 제거
        {
            keySprite.SetActive(false);
        }
    }
}
