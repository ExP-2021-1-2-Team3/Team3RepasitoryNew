using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
    public GameObject[] dawnObject;
    public Transform clockObject;
    public GameObject glitchBG;
    public GameObject normalBG;
    public GameObject[] animations;
    AudioSource audioSource;
    GameObject postProcessObject;

    LensDistortion lensDistortion;
    Grain grain;
    ChromaticAberration chrome;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        glitchBG.SetActive(false);
        normalBG.SetActive(true);
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
                    //coll.isTrigger = false;
                    coll.enabled = false;
                    rigid.AddForce(new Vector2(Random.Range(-1, 1), 0),ForceMode2D.Impulse);
                    rigid.AddTorque(Random.Range(-3,3),ForceMode2D.Impulse);
                    rigid.gravityScale = 1;
                    nowClickedGlitch++;
                    if (nowClickedGlitch >= glitchNumber)
                    {
                        StartCoroutine(GlitchEndCoroutine());
                    }
                    if(nowClickedGlitch == 1)
                    {
                        StartCoroutine(FirstCor());
                    }
                    else if(nowClickedGlitch == 2)
                    {
                        StartCoroutine(SecondCor());
                    }
                    else if(nowClickedGlitch == 3)
                    {
                        ThirdCor();
                    }
                    else if(nowClickedGlitch == 4)
                    {
                        animations[3].SetActive(true);
                        StartCoroutine(PostProcessCoroutine());
                    }
                    
                }
            }
        }

    }

    IEnumerator FirstCor()
    {
        animations[0].SetActive(true);
        yield return new WaitForSeconds(2f);
        animations[0].SetActive(false);
    }
    IEnumerator SecondCor()
    {
        animations[1].SetActive(true);
        yield return new WaitForSeconds(2f);
        animations[1].SetActive(false);
    }
    void ThirdCor()
    {
        animations[0].SetActive(true);
        animations[2].SetActive(true);
        normalBG.SetActive(false);
        glitchBG.SetActive(true);

    }


    IEnumerator PostProcessCoroutine()
    {
        postProcessObject = GameObject.Find("postStartEffect");
        lensDistortion = postProcessObject.GetComponent<PostProcessVolume>().profile.GetSetting<LensDistortion>();
        grain = postProcessObject.GetComponent<PostProcessVolume>().profile.GetSetting<Grain>();
        chrome = postProcessObject.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>();
        grain.intensity.value = 0;
        chrome.intensity.value = 0;
        lensDistortion.intensity.value = 0;

        float targetLens = 80;
        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime / 6;
            grain.intensity.value = timer;
            chrome.intensity.value = timer;
            lensDistortion.intensity.value = timer * targetLens;
            yield return null;
        }
        grain.intensity.value = 0;
        chrome.intensity.value = 0;
        lensDistortion.intensity.value = 0;
    }


    IEnumerator GlitchEndCoroutine()
    {

        yield return new WaitForSeconds(6f);
        fadeObject.SetActive(true);
        glitchParent.SetActive(false);
        for(int i = 0; i < animations.Length; i++)
        {
            animations[i].SetActive(false);

        }
        normalBG.SetActive(true);
        glitchBG.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        audioSource.Play();
        yield return new WaitForSeconds(4f);
        fadeObject.SetActive(false);
        float timer = 0;
        dawnObject[0].SetActive(true);
        clockObject.gameObject.SetActive(true);
        while (timer < 1)
        {
            timer += Time.deltaTime/1.5f;
            clockObject.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
            yield return null;
        }
        timer = 0;
        dawnObject[0].SetActive(false);
        dawnObject[1].SetActive(true);
        while (timer < 1)
        {
            timer += Time.deltaTime/1.5f;
            clockObject.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
            yield return null;
        }
        timer = 0;
        dawnObject[1].SetActive(false);
        dawnObject[2].SetActive(true);
        while (timer < 1)
        {
            timer += Time.deltaTime/2;
            clockObject.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
            yield return null;
        }
        timer = 0;
        dawnObject[2].SetActive(false);


        
        
        fadeObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        titleObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
