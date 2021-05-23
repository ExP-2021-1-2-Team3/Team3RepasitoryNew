using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//화면에 띄워질 글자 1개 오브젝트
public class LetterUiHS : MonoBehaviour
{

    [SerializeField] private int lengthOfThisObject;
    [SerializeField] private int gap;
    [SerializeField] private int height;

    private Text childText;
    private bool fading;

    int getRectXPosition(int index, int max){
        return (int)((lengthOfThisObject + gap) * (index + (3.5f - max / 2.0f)));
    }

    public void SetPositionAndChar(int index, int max, char this_letter){
        Color text_color = new Color(0, 0, 0, 0);
        Image image = GetComponent<Image>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(getRectXPosition(index, max), height, 0);
        if(childText == null)
            childText = transform.GetChild(0).GetComponent<Text>();
        childText.text = this_letter.ToString();
        childText.color = text_color;
        image.color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        fading = false;
        childText = transform.GetChild(0).GetComponent<Text>();
    }

    //글자가 맞았을 때 코루틴 불러주는 함수
    public void Correct(){
        StartCoroutine(Appear());
    }

    public bool IsFading(){
        return fading;
    }

    //스프라이트는 페이드 아웃, 텍스트는 페이드 인
    public IEnumerator Appear()
    {
        float t = 0f;
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        Image image = GetComponent<Image>();
        Color color = new Color(1, 1, 1, 1), text_color = new Color(0, 0, 0, 0);
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
