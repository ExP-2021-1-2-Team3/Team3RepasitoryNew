using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//화면에 띄워지는 글자들 조절하는 애
public class LetterUiManagerHS : MonoBehaviour
{

    //나올 수 있는 단어의 최대 길이 여기서는 5을 적어놓음.
    [SerializeField] int maxLength;
    [SerializeField] private GameObject pre_UILetter;
    [SerializeField] private PersonHS person;
    //일단 테스트를 위해서 BASEBALL을 넣어 놓음
    private string currentWord = "BASEBALL";
    private int correctIndex, inputIndex;
    private bool isUIupdateNeeded = false;

    private List<LetterUiHS> letterUIList;

    public bool IsCurrentWordInputOver(){
        return currentWord.Length <= inputIndex;
    }

    public bool IsCurrentWordIsCorrect(){
        return currentWord.Length <= correctIndex;
    }

    public void InputChar(char input_char)
    {
        //단어의 마지막 자가 페이딩 처리중(아무런 입력도 받지 않음)
        if(letterUIList[currentWord.Length - 1].IsFading())
            return;

        letterUIList[inputIndex].Input(input_char);
        //정답을 맞춤.
        if(currentWord[inputIndex] == input_char){
            correctIndex += 1;
            person.Good();
        } //정답을 못 맞춤.
        else { 
            person.Angry();
        }
        inputIndex += 1;
    }

    public bool SetGameString(string setString){
        //단어의 마지막 글자를 맞추어서 페이딩 처리중
        if(letterUIList != null){
            if(letterUIList[currentWord.Length - 1].IsFading())
                return false;
        }
        
        correctIndex = 0;
        inputIndex = 0;
        currentWord = setString;
        isUIupdateNeeded = true;

        return true;
    }

    //SetGameString의 이후, 혹은 틀렸을때 InputChar에서 다이렉트로 불린다.
    //굳이 플래그를 세워서 글자들을 나중에 업데이트 하는 이유는 처음 scene로딩을 하는 중에는
    //letterUIList에 아무것도 들어있지 않아 제대로된 업데이트가 불가능하기 때문.
    //적어도 Start가 불린 이후에야 UIList업데이트를 통해서 플레이어에게 보여줄 수 있다.
    void UpdateUiObjects(){        
        for(int i = 0; i < letterUIList.Count; i++){
            if(i < currentWord.Length){
                letterUIList[i].ResetUi(i, currentWord.Length, gameObject.GetComponent<RectTransform>());
                letterUIList[i].gameObject.SetActive(true);
                letterUIList[i].AnimateReset();
            } else {
                letterUIList[i].gameObject.SetActive(false);
            }
        }
        isUIupdateNeeded = false;
    }

    void Start()
    {
        if(instance == null)
            instance = this;
        
        correctIndex = 0;
        inputIndex = 0;
        letterUIList = new List<LetterUiHS>();

        Transform canvas = GameObject.FindGameObjectWithTag("CanvasHS").transform;
        for(int i = 0; i < maxLength; i++){
            LetterUiHS letterUiHS = Instantiate(pre_UILetter, Vector2.zero, transform.rotation, canvas).GetComponent<LetterUiHS>();
            letterUiHS.gameObject.SetActive(false);
            letterUIList.Add(letterUiHS);
        }
    }

    void Update()
    {
        if(isUIupdateNeeded)
            UpdateUiObjects();
        //정답이 아니다. MainGameHS에서 부르는 형태를 비슷하게 이용함.
        if(IsCurrentWordInputOver() && !IsCurrentWordIsCorrect()){
            SetGameString(currentWord);
        }
    }

    public static LetterUiManagerHS GetInstance()
    {
        if(instance == null){
            Debug.Log("여기서 이게 불리면 안됩니다.");
            //instance = new LetterUiManagerHS();
            return null;
        }
        return instance;
    }

    private static LetterUiManagerHS instance;

    private LetterUiManagerHS(){
        instance = this;
    }
}
