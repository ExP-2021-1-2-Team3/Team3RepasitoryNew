using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//메인 게임 스크립트
public class MainGameHS : MonoBehaviour
{

    [Header("Set In Editor")]
    [SerializeField] private GameObject pre_letter;
    //최대 글자 생성 개수
    [SerializeField] public int maxLetter;
    //글자 스폰 주기
    [SerializeField] public float period = 0.1f;
    
    //여기는 인스펙터에서 건드리면 안되는 변수들(어짜피 private)
    private string[] game = {"BOOK","BEAR","ALARM",""};
    private string[] phase = {"BOK","BEAROKLY","ALRMBEOKYPX","A"};
    private float nextActionTime = 0.0f;
    private List<LetterHS> letterList;
    private Image background;
    private int nextSpwanIndex, nextGameIndex;
    private bool isGameOver;

    public void AddToList(LetterHS letterHS)
    {
        letterList.Add(letterHS);
    }
    
    void Start(){
        letterList = new List<LetterHS>(); //글자들 떨어지는거 오브젝트 풀링
        background = gameObject.GetComponent<Image>(); //배경화면 전환
        nextSpwanIndex = 0; //리스트 순회용
        nextGameIndex = 0; //BEAR, BOOK, ALARM용
        isGameOver = false;

        //미리 maxLetter개 만큼 하늘에서 떨어지는 글자 생성
        for(int i = 0; i < maxLetter; i++){
            LetterHS new_letter = Instantiate(pre_letter, Vector2.zero, transform.rotation).GetComponent<LetterHS>();
            new_letter.gameObject.SetActive(false);
            //new_letter.SetMainGameHS(this);
            letterList.Add(new_letter);
        }
        LoadNextGame();
    }

    void LoadNextGame(){
        if (isGameOver)
        {
           
            return;
        }//게임이 끝났으면 더이상의 게임을 로드하지 않는다.
            
        
        if(LetterUiManagerHS.GetInstance().SetGameString(game[nextGameIndex])){
            //각 게임이 끝나서 새롭게 게임 스트링을 지정했을 때, 뒤의 이미지들 역시 생기게 한다.
            switch(nextGameIndex){
                case 0:
                    background.sprite = Resources.Load<Sprite>("HS_1");
                    break;
                case 1:
                    background.sprite = Resources.Load<Sprite>("HS_2");
                    break;
                case 2:
                    background.sprite = Resources.Load<Sprite>("HS_3");
                    break;
                case 3:
                    background.sprite = Resources.Load<Sprite>("HS_4");
                    break;
                default:
                    break;
            }
            
            if(nextGameIndex >= 3){
                Debug.Log("GAME END");
                LoadManagerSH.singleTon.GameEnd();
                isGameOver = true;
            } else {
                nextGameIndex += 1;
            }
        }   
    }

    //마지막 SceneLoad, 주인공이 모든 글자를 맞추고 알람시계를 눌렀다.
    //여기가 클리어 조건입니다. SceneManager.LoadScene("MainRoom")
    //이곳에다가 씬 넘어가는 코드 짜주시면 됩니다.
    public void OnAlarmClockClick(){
        if(isGameOver){
            SceneManager.LoadScene("MainRoom");
        }
    }

    //현재 진행중인 게임이 끝났으면 다음 게임으로
    void Update(){
        if(LetterUiManagerHS.GetInstance().IsCurrentWordIsCorrect()){
            LoadNextGame();
        }
    }

    //그 다음 리스트 순회 인덱스 가져오기
    private int GetNextIndex()
    {
        nextSpwanIndex += 1;
        if(nextSpwanIndex >= maxLetter)
            nextSpwanIndex = 0;
        return nextSpwanIndex;
    }

    //리스트에서 가져온다!
    private void SpawnNewLetter()
    {
        //페이즈 기능 BOK -> BEAROKLY -> ALRMBEOKYPX
        char new_char = '?';
        new_char = phase[nextGameIndex - 1][Random.Range(0, phase[nextGameIndex - 1].Length)];

        LetterHS next_letter = letterList[GetNextIndex()];
        next_letter.ResetByChar(new_char);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //random letters spawner!
        if (!isGameOver && Time.time > nextActionTime){
            nextActionTime += period;
            SpawnNewLetter();
        }
    }
}
