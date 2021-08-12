using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    void Start()
    {
        loadManager = LoadManagerSH.singleTon;
        if (loadManager.nextStage == 5)
        {
                glitchTitle.sortingOrder = 4;

        }
        MakeGlitch();
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
        StartCoroutine(loadManager.GameStartCor(true));
    }

    public IEnumerator SceneLoadAnimation()
    {
        uiCanvas.SetActive(false);
        MakeGlitch();
        yield return new WaitForSeconds(2f);
        uiCanvas.SetActive(true);
    }

    void MakeGlitch()
    {
        nextStage = loadManager.nextStage;
        if(nextStage == 1 || nextStage == 2)
        {
            return;
        }
        if (nextStage > 4)
        {
            normalBackGround.SetActive(false);
            
            return;
        }
        StartCoroutine(GlitchCoroutine(nextStage - 1));
    }

    IEnumerator GlitchCoroutine(int index)
    {
        SpriteRenderer[] childrens = new SpriteRenderer[3];
        Debug.Log("여러번");
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < index; i++)
        {
            childrens[i] = glitchArray[i].GetComponent<SpriteRenderer>();
            //childrens[i].gameObject.SetActive(true);
        }
        while (true)
        {

            //for (int j = 0; j < index; j++)
            //{
            //    childrens[j].gameObject.SetActive(true);
            //    //childrens[j].color = new Color(1, 1, 1, Random.Range(0, 1f));
            //}
            //for (int i = 0; i < index; i++)
            //{
            //    childrens[i].color = new Color(1, 1, 1, Random.Range(0, 1f));
            //    for (int j = 0; j < 4 * (nextStage - 1); j++)
            //    {
            //        if (Random.Range(0, 2) == 0)
            //        {
            //            continue;
            //        }
            //        maskParent.transform.GetChild(j).gameObject.SetActive(true);
            //    }
            //    yield return new WaitForSeconds(5f);
            //    for (int j = 0; j < 4 * (nextStage - 1); j++)
            //    {
            //        maskParent.transform.GetChild(j).gameObject.SetActive(false);
            //    }
            //    for(int j = 0; j < index; j++)
            //    {
            //        childrens[j].gameObject.SetActive(false);
            //    }
            //}

            for (int j = 0; j < 4 * (nextStage - 1); j++)
            {
                maskParent.transform.GetChild(j).gameObject.SetActive(true);
            }
            childrens[0].gameObject.SetActive(true);
             yield return null; yield return null; yield return null;
            childrens[0].gameObject.SetActive(false);
            childrens[1].gameObject.SetActive(true);
            for (int j = 0; j < 4 * (nextStage - 1); j++)
            {
                maskParent.transform.GetChild(j).gameObject.SetActive(false);
            }

            for (int j = 4 * (nextStage - 1); j < 4 * (nextStage); j++)
            {
                maskParent.transform.GetChild(j).gameObject.SetActive(true);
            }

            yield return null; yield return null; yield return null;
            childrens[1].gameObject.SetActive(false);
            for (int j = 4 * (nextStage - 1); j < 4 * (nextStage); j++)
            {
                maskParent.transform.GetChild(j).gameObject.SetActive(false);
            }
            if (nextStage > 3)
            {
                childrens[1].gameObject.SetActive(false);
                childrens[2].gameObject.SetActive(true);
                for (int j = 4 * (nextStage-1); j < 4 * (nextStage); j++)
                {
                    maskParent.transform.GetChild(j).gameObject.SetActive(true);

                }
                yield return new WaitForSeconds(0.1f);
                childrens[2].gameObject.SetActive(false);
                for (int j = 4 * (nextStage - 1); j < 4 * (nextStage); j++)
                {
                    maskParent.transform.GetChild(j).gameObject.SetActive(false);

                }
            }




            for (int j = 0; j < index; j++)
            {
                childrens[j].gameObject.SetActive(false);
            }

            for (int j = 0; j < 4 * (nextStage - 1); j++)
            {
                maskParent.transform.GetChild(j).gameObject.SetActive(false);
                
            }
            for (int j = 0; j < index; j++)
            {
                childrens[j].gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
