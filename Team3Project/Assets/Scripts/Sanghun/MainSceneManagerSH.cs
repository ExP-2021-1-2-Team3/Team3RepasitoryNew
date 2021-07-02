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
    GameObject[] glitchArray;
    void Start()
    {
        loadManager = LoadManagerSH.singleTon;
        MakeGlitch();
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
        for (int i = 0; i < index; i++)
        {
            childrens[i] = glitchArray[i].GetComponent<SpriteRenderer>();
            childrens[i].gameObject.SetActive(true);
        }
        while (true)
        {
            for(int i = 0; i < index; i++)
            {
                childrens[i].color = new Color(1, 1, 1, Random.Range(0, 1f));
                for (int j = 0; j < 8 * (nextStage - 1); j++)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        continue;
                    }
                    maskParent.transform.GetChild(j).gameObject.SetActive(true);
                }
                yield return new WaitForSeconds(0.3f);
                for (int j = 0; j < 8 * (nextStage - 1); j++)
                {
                    maskParent.transform.GetChild(j).gameObject.SetActive(false);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
