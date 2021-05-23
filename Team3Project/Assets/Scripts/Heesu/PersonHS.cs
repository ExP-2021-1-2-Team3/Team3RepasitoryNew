using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonHS : MonoBehaviour
{
    [SerializeField] public float duration;
    private Image image;
    private Sprite noraml, angry, good;
    private float startTime;
    private bool doFaceReveal;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        startTime = Time.time;
        doFaceReveal = false;
        noraml = Resources.Load<Sprite>("Single_Box");
        good = Resources.Load<Sprite>("Single_Diff");
        angry = Resources.Load<Sprite>("Single_Clock");
    }

    // Update is called once per frame
    void Update()
    {
        if (doFaceReveal && Time.time > startTime){
            doFaceReveal = false;
            image.sprite = noraml;
        }
    }

    public void Angry()
    {
        image.sprite = angry;
        doFaceReveal = true;
        startTime = Time.time + duration;
    }

    public void Good()
    {
        image.sprite = good;
        doFaceReveal = true;
        startTime = Time.time + duration;
    }
}
