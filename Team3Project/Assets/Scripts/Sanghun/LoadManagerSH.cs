﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManagerSH : MonoBehaviour
{
    public static LoadManagerSH singleTon;
    MainSceneManagerSH mainSceneManager;
    [SerializeField]
    GameObject fadeCanvas;
    [SerializeField]
    GameObject clockButtonCanvas;
    [SerializeField]
    GameObject clockImageCanvas;

    AudioSource ringAudio;
    public int nextStage;
    // Start is called before the first frame update
    void Awake()
    {
        if (singleTon == null)
        {
            singleTon = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(fadeCanvas);
            DontDestroyOnLoad(clockButtonCanvas);
            DontDestroyOnLoad(clockImageCanvas);
        }
        else
        {
            Destroy(fadeCanvas);
            Destroy(clockButtonCanvas);
            Destroy(clockImageCanvas);
            Destroy(gameObject);
        }
        ringAudio = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("Stage"))
        {
            PlayerPrefs.SetInt("Stage", 1);
            PlayerPrefs.Save();
        }
        //nextStage = PlayerPrefs.GetInt("Stage");
        nextStage = 1;
        StartCoroutine(GameStartCor(false));

    }

    public void LoadHomeScene()
    {
        nextStage++;
        PlayerPrefs.SetInt("Stage", nextStage);
        PlayerPrefs.Save();
     
        SceneManager.LoadScene(0);
        
    }

    //페이드인인지 아웃인지, 스테이지 로드하는지 안하는지, 집으로 로드하는지 게임씬 로드인지,
    //게임씬이면 현재 스테이지 인덱스 몇인지.
    public IEnumerator GameStartCor(bool fadeIn)
    {
        fadeCanvas.SetActive(true);
        Image fadeImage = fadeCanvas.GetComponentInChildren<Image>();
        float timer = 0;
        Color color;
        if (fadeIn)
        {
            color = new Color(0, 0, 0, 0);
        }
        else
        {
            color = new Color(0, 0, 0, 1);
        }

        while (timer < 1)
        {
            timer += Time.deltaTime;
            if (fadeIn)
            {
                color.a = timer;
            }
            else
            {
                color.a = 1 - timer;
            }
            fadeImage.color = color;
            yield return null;
        }
        if (fadeIn)
        {
            color.a = 1;
        }
        else
        {
            color.a = 0;
        }
        fadeImage.color = color;
        fadeCanvas.SetActive(false);
        
        if(fadeIn)
        {

            StartCoroutine(WaitAndPop());
            LoadGameScene();
        }
    }

    IEnumerator WaitAndPop()
    {
        fadeCanvas.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeCanvas.SetActive(false);
    }

    public void GameEnd()
    {
        clockButtonCanvas.SetActive(true);
    }

    public void OnClockButton()
    {
        clockButtonCanvas.SetActive(false);
        clockImageCanvas.SetActive(true);
        StartCoroutine(ClockRingCor());
    }

    public IEnumerator ClockRingCor()
    {
        RectTransform rect = clockImageCanvas.transform.GetChild(0).GetComponent<RectTransform>();

        float x = 0;
        float y = 0;
        float timer = 0;
        ringAudio.Play();
        while(timer < 2)
        {
            timer += Time.deltaTime;
            x = Random.Range(-100, 100);
            y = Random.Range(-100, 100);

            rect.anchoredPosition = new Vector2(x, y);
            yield return null;
        }
        clockImageCanvas.SetActive(false);

        ringAudio.Stop();
        StartCoroutine(GameEndCor());
    }

    public IEnumerator GameEndCor()
    {
        fadeCanvas.SetActive(true);
        Image fadeImage = fadeCanvas.GetComponentInChildren<Image>();
        fadeImage.color = Color.black;
        LoadHomeScene();
        yield return new WaitForSeconds(2f);
        mainSceneManager = GameObject.Find("MainSceneManager").GetComponent<MainSceneManagerSH>();
        StartCoroutine(mainSceneManager.SceneLoadAnimation());
        fadeCanvas.SetActive(false);
    }



    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(nextStage);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}