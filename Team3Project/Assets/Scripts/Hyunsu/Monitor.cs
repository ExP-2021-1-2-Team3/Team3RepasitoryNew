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
        }   //  ����ĳ��Ʈ�� ������Ʈ ����

        if (touchedObj == aimObj)   // ���õ� ������Ʈ = ����� �� ��� ����� ���� ȭ�� ���
        {
            computerSound.Play();
            monitorJoomin.SetActive(true);
            CloseBtn.isOpenedUI = true;
            touchedObj = null;
        }
    }
}