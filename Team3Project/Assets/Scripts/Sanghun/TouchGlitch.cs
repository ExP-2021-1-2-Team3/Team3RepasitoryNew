﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGlitch : MonoBehaviour
{
    LoadManagerSH loadManager;
    public Camera cam;
    GameObject touchedObject;
    RaycastHit2D hit;
    int glitchNumber = 4;
    int nowClickedGlitch = 0;
    public GameObject glitchParent;
    public GameObject fadeObject;
    public GameObject titleObject;
    public GameObject dawnObject;
    public Transform clockObject;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
            if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
            {
                touchedObject = hit.collider.gameObject;

                //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                if (touchedObject.CompareTag("Glitch"))
                {
                    Rigidbody2D rigid= touchedObject.GetComponent<Rigidbody2D>();
                    BoxCollider2D coll = touchedObject.GetComponent<BoxCollider2D>();
                    coll.enabled = false;
                    rigid.AddForce(new Vector2(Random.Range(-4, 4), 0),ForceMode2D.Impulse);
                    rigid.AddTorque(Random.Range(4,7),ForceMode2D.Impulse);
                    rigid.gravityScale = 1;
                    nowClickedGlitch++;
                    if (nowClickedGlitch >= glitchNumber)
                    {
                        StartCoroutine(GlitchEndCoroutine());
                    }
                    
                }
            }
        }

    }

    IEnumerator GlitchEndCoroutine()
    {

        yield return new WaitForSeconds(3f);
        fadeObject.SetActive(true);
        glitchParent.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        fadeObject.SetActive(false);
        float timer = 0;
        dawnObject.SetActive(true);
        clockObject.gameObject.SetActive(true);
        while (timer < 4)
        {
            timer += Time.deltaTime;
            clockObject.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
            yield return null;
        }
        
        
        fadeObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        titleObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
