using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonHS : MonoBehaviour
{
    [SerializeField] public float duration;
    private Image image, ex_image;
    private Sprite noraml, angry;
    private float startTime;
    private bool return_normal_flag;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        ex_image = gameObject.transform.GetChild(0).GetComponent<Image>();
        ex_image.color = new Color(1, 1, 1, 0);
        startTime = Time.time;
        return_normal_flag = false;
        noraml = Resources.Load<Sprite>("cha1");
        angry = Resources.Load<Sprite>("cha2");
    }

    // Update is called once per frame
    void Update()
    {
        if (return_normal_flag && Time.time > startTime){
            image.sprite = noraml;
            ex_image.color = new Color(1, 1, 1, 0);
            return_normal_flag = false;
        }
    }

    public void Angry()
    {
        image.sprite = angry;
        if(ex_image.color.a > 0.5f)
            ex_image.color = new Color(1, 1, 1, 1);
        return_normal_flag = true;
        startTime = Time.time + duration;
    }

    public void Good()
    {
        ex_image.color = new Color(1, 1, 1, 1);
        if(image.sprite.name == "cha2")
            image.sprite = noraml;
        return_normal_flag = true;
        startTime = Time.time + duration;
    }
}
