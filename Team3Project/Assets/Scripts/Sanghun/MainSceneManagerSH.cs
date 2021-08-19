using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MainSceneManagerSH : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject uiCanvas;
    LoadManagerSH loadManager;
    int nextStage;
    [SerializeField]
    GameObject maskParent;
    [SerializeField]
    GameObject normalBackGround;

    [SerializeField]
    GameObject settingCanvas;
    [SerializeField]
    GameObject creditCanvas;
    [SerializeField]
    SpriteRenderer glitchTitle;

    [SerializeField]
    GameObject[] glitchArray;
    [SerializeField]
    GameObject[] glitchAnimationArray;
    [SerializeField]
    GameObject lightningObject;
    GameObject[] activeGlitchArray;

    [SerializeField]
    SpriteRenderer upFace;
    [SerializeField]
    SpriteRenderer downFace;

    [SerializeField]
    SpriteRenderer normalBG;

    [SerializeField]
    Sprite[] normalBGSprite;
    [SerializeField]
    Sprite[] upFaceSprite;
    [SerializeField]
    Sprite[] downFaceSprite;




    void Start()
    {

        loadManager = LoadManagerSH.singleTon;
        if (loadManager.nextStage >=4)
        {
            glitchTitle.sortingOrder = 4;
        }
        MakeGlitch();
        StartCoroutine(LightningCoroutine());
        normalBG.sprite = normalBGSprite[1];
        upFace.gameObject.SetActive(false);
    }

    public void OnSettingButton(bool active)
    {
        uiCanvas.SetActive(!active);
        settingCanvas.SetActive(active);
    }

    public void OnCredtButton(bool active)
    {
        uiCanvas.SetActive(!active);
        creditCanvas.SetActive(active);
    }

    public void GameStartButton()
    {
        uiCanvas.SetActive(false);
        downFace.sprite = downFaceSprite[1];
        StartCoroutine(loadManager.GameStartCor(true));
    }

    public IEnumerator SceneLoadAnimation()
    {
        uiCanvas.SetActive(false);
        stopGlitch = true;
        if (nextStage <= 3)
        {
            normalBG.sprite = normalBGSprite[0];
            downFace.gameObject.SetActive(false);
            upFace.gameObject.SetActive(true);
            upFace.sprite = upFaceSprite[0];
            yield return new WaitForSeconds(1f);
            upFace.sprite = upFaceSprite[1];
            yield return new WaitForSeconds(1f);
            upFace.sprite = upFaceSprite[2];
            yield return new WaitForSeconds(1.5f);
            normalBG.sprite = normalBGSprite[1];
            upFace.gameObject.SetActive(false);
            downFace.gameObject.SetActive(true);
            downFace.sprite = downFaceSprite[0];
        }

        yield return new WaitForSeconds(1.5f);

        MakeGlitch();
        stopGlitch = false;
        Image[] imageArray = uiCanvas.GetComponentsInChildren<Image>();
        Text[] textArray = uiCanvas.GetComponentsInChildren<Text>();

        float timer = 0;
        uiCanvas.SetActive(true);
        while (timer < 1)
        {
            for(int i = 0; i < imageArray.Length; i++)
            {
                imageArray[i].color = new Color(1, 1, 1, timer);
            }
            for (int i = 0; i < textArray.Length; i++)
            {
                textArray[i].color = new Color(0, 0, 0, timer);
            }
            timer += Time.deltaTime;
            yield return null;
        }


    }

    void MakeGlitch()
    {
        nextStage = loadManager.nextStage;

        for(int i = 0; i < glitchAnimationArray.Length; i++)
        {
            glitchAnimationArray[i].SetActive(false);
        }
        if(nextStage == 1 || nextStage == 2)
        {
            return;
        }
        if (nextStage == 3)
        {
            activeGlitchArray = new GameObject[3];
            activeGlitchArray[0] = glitchAnimationArray[2];
            activeGlitchArray[1] = glitchAnimationArray[3];
            activeGlitchArray[2] = glitchAnimationArray[4];
        }
        if (nextStage == 4)
        {
            activeGlitchArray = new GameObject[2];
            activeGlitchArray[0] = glitchAnimationArray[5];
            activeGlitchArray[1] = glitchAnimationArray[6];
            
            normalBackGround.SetActive(false);
        }
        if(nextStage == 5)
        {
            activeGlitchArray = new GameObject[2];
            activeGlitchArray[0] = glitchAnimationArray[0];
            activeGlitchArray[1] = glitchAnimationArray[1];

            normalBackGround.SetActive(false);
        }
        StartCoroutine(GlitchCoroutine());
    }

    void ActiveGlitchAnimation(int index,bool active)
    {
        glitchAnimationArray[index].SetActive(active);
    }


    
    IEnumerator LightningCoroutine()
    {
        while (true)
        {
            lightningObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            lightningObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            lightningObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            lightningObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            lightningObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    bool stopGlitch = false;
    IEnumerator GlitchCoroutine()
    {
        while (!stopGlitch)
        {
            for(int i = 0; i < activeGlitchArray.Length; i++)
            {
                activeGlitchArray[i].SetActive(true);
                yield return new WaitForSeconds(Random.Range(3f, 5f));
                activeGlitchArray[i].SetActive(false);
            }
        }
    }

    //IEnumerator GlitchCoroutine(int index)
    //{
    //    SpriteRenderer[] childrens = new SpriteRenderer[3];
    //    Debug.Log("여러번");
    //    yield return new WaitForSeconds(3f);
    //    for (int i = 0; i < index; i++)
    //    {
    //        childrens[i] = glitchArray[i].GetComponent<SpriteRenderer>();
    //        //childrens[i].gameObject.SetActive(true);
    //    }
    //    while (true)
    //    {

    //        //for (int j = 0; j < index; j++)
    //        //{
    //        //    childrens[j].gameObject.SetActive(true);
    //        //    //childrens[j].color = new Color(1, 1, 1, Random.Range(0, 1f));
    //        //}
    //        //for (int i = 0; i < index; i++)
    //        //{
    //        //    childrens[i].color = new Color(1, 1, 1, Random.Range(0, 1f));
    //        //    for (int j = 0; j < 4 * (nextStage - 1); j++)
    //        //    {
    //        //        if (Random.Range(0, 2) == 0)
    //        //        {
    //        //            continue;
    //        //        }
    //        //        maskParent.transform.GetChild(j).gameObject.SetActive(true);
    //        //    }
    //        //    yield return new WaitForSeconds(5f);
    //        //    for (int j = 0; j < 4 * (nextStage - 1); j++)
    //        //    {
    //        //        maskParent.transform.GetChild(j).gameObject.SetActive(false);
    //        //    }
    //        //    for(int j = 0; j < index; j++)
    //        //    {
    //        //        childrens[j].gameObject.SetActive(false);
    //        //    }
    //        //}

    //        for (int j = 0; j < 4 * (nextStage - 1); j++)
    //        {
    //            maskParent.transform.GetChild(j).gameObject.SetActive(true);
    //        }
    //        childrens[0].gameObject.SetActive(true);
    //         yield return null; yield return null; yield return null;
    //        childrens[0].gameObject.SetActive(false);
    //        childrens[1].gameObject.SetActive(true);
    //        for (int j = 0; j < 4 * (nextStage - 1); j++)
    //        {
    //            maskParent.transform.GetChild(j).gameObject.SetActive(false);
    //        }

    //        for (int j = 4 * (nextStage - 1); j < 4 * (nextStage); j++)
    //        {
    //            maskParent.transform.GetChild(j).gameObject.SetActive(true);
    //        }

    //        yield return null; yield return null; yield return null;
    //        childrens[1].gameObject.SetActive(false);
    //        for (int j = 4 * (nextStage - 1); j < 4 * (nextStage); j++)
    //        {
    //            maskParent.transform.GetChild(j).gameObject.SetActive(false);
    //        }
    //        if (nextStage > 3)
    //        {
    //            childrens[1].gameObject.SetActive(false);
    //            childrens[2].gameObject.SetActive(true);
    //            for (int j = 4 * (nextStage-1); j < 4 * (nextStage); j++)
    //            {
    //                maskParent.transform.GetChild(j).gameObject.SetActive(true);

    //            }
    //            yield return new WaitForSeconds(0.1f);
    //            childrens[2].gameObject.SetActive(false);
    //            for (int j = 4 * (nextStage - 1); j < 4 * (nextStage); j++)
    //            {
    //                maskParent.transform.GetChild(j).gameObject.SetActive(false);

    //            }
    //        }




    //        for (int j = 0; j < index; j++)
    //        {
    //            childrens[j].gameObject.SetActive(false);
    //        }

    //        for (int j = 0; j < 4 * (nextStage - 1); j++)
    //        {
    //            maskParent.transform.GetChild(j).gameObject.SetActive(false);
                
    //        }
    //        for (int j = 0; j < index; j++)
    //        {
    //            childrens[j].gameObject.SetActive(false);
    //        }

    //        yield return new WaitForSeconds(3f);
    //    }
    //}
    

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
