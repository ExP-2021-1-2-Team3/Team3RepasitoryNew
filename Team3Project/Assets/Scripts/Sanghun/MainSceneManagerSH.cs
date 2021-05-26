using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        loadManager = LoadManagerSH.singleTon;
    }

    public void GameStartButton()
    {
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
        if (nextStage > 4)
        {
            normalBackGround.SetActive(false);
            return;
        }
        for(int i = 0; i < 8*(nextStage-1); i++)
        {
            maskParent.transform.GetChild(i).gameObject.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
