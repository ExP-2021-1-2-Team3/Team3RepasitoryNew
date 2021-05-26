using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//화면에 띄워질 글자 1개 오브젝트
public class LetterUiHS : MonoBehaviour
{

    [SerializeField] private int lengthOfThisObject;
    [SerializeField] private int gap;

    private Text childText;
    private bool fading;
    private bool will_input, will_reset;
    private char will_input_char;

    int getRectXPosition(int index, int currMaxLength){
        return (int)((lengthOfThisObject + gap) * (index + (5 - currMaxLength) / 2.0f));
    }

    public void ResetUi(int index, int currMaxLength, RectTransform parent){
        Image image = GetComponent<Image>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(
            parent.anchoredPosition.x - parent.rect.width * 0.5f + getRectXPosition(index, currMaxLength) - 55, 
            parent.anchoredPosition.y - 20, 0);
        if(childText == null)
            childText = transform.GetChild(0).GetComponent<Text>();
        childText.color = new Color(0, 0, 0, 0);
        image.color = new Color(1, 1, 1, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        fading = false;
        childText = transform.GetChild(0).GetComponent<Text>();
    }

    void Update(){
        if(will_input)
            Input(will_input_char);
        if(will_reset)
            AnimateReset();
    }

    //글자가 맞았을 때 코루틴 불러주는 함수
    public void Input(char input){
        if(fading){
            will_input_char = input;
            will_input = true;
        } 
        else {
            StartCoroutine(Appear(input));
            will_input = false;
        }
    }

    public void AnimateReset(){
        if(fading){
            will_reset = true;
        } 
        else {
            StartCoroutine(ResetCo());
            will_reset = false;
        }    
    }

    public bool IsFading(){
        return fading;
    }

    public IEnumerator ResetCo()
    {
        float t = 0f;
        bool do_animate_text_alpha = false;
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        Image image = GetComponent<Image>();
        Color color = new Color(1, 1, 1, 0), text_color = new Color(0, 0, 0, 0);
        fading = true;

        //처음 불린 경우가 아닌 경우 텍스트를 애니메이팅함.
        if(text_color.a > 0.0f)
            do_animate_text_alpha = true;

        //앱실론 - 델타 논법의 그 앱실론인듯?
        //작은 양의 실수. 오차값의 최대값이다. float 계열의 0이라고 생각해라.
        while(Mathf.Abs(1.0f - color.a) > Mathf.Epsilon){
            yield return w; //제동장치. 아니면 시스템 클럭 속도로 돌아간다.

            t += Time.deltaTime;

            color.a = Mathf.Lerp(color.a, 1.0f, t);
            image.color = color;

            if(do_animate_text_alpha){
                text_color.a = 1.0f - color.a;
                childText.color = text_color;
            }    
        }

        image.color = Color.white;
        if(do_animate_text_alpha)
            childText.color = new Color(0, 0, 0, 0);
        fading = false;
    }

    //스프라이트는 페이드 아웃, 텍스트는 페이드 인
    public IEnumerator Appear(char input)
    {
        float t = 0f;
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        Image image = GetComponent<Image>();
        Color color = new Color(1, 1, 1, 1), text_color = new Color(0, 0, 0, 0);
        childText.text = input.ToString();
        fading = true;

        //앱실론 - 델타 논법의 그 앱실론인듯?
        //작은 양의 실수. 오차값의 최대값이다. float 계열의 0이라고 생각해라.
        while(Mathf.Abs(color.a) > Mathf.Epsilon){
            yield return w; //제동장치. 아니면 시스템 클럭 속도로 돌아간다.

            t += Time.deltaTime;

            color.a = Mathf.Lerp(color.a, 0.0f, t);
            text_color.a = 1.0f - color.a;

            image.color = color;
            childText.color = text_color;
        }

        fading = false;
        childText.color = Color.black;
    }
}
