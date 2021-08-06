using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerJH : MonoBehaviour
{
    //1. 스턴까지 걸리는 시간을 카운트를 한다.
    //2. 또한 리스폰을 관리한다.
    //3. 체력에 영향을 미치는 요인을 통제한다.
    //4. 게임 클리어, 게임 오버를 관리한다.

    //카운트다운이 끝나면
    //플레이어의 움직임(벡터)을 최대 5초동안 0으로 만든다.-> player.rigid.velocity=Vector3.zero;

    //그동안
    //case 1. LeftBtn, RightBtn 을 번갈아서 15번 클릭해야 빠져나옴.
    //case 2. JumpBtn을 20번 클릭해야 빠져나옴.
    //case 3. 상호작용Btn을 20번 클릭해야 빠져나옴. 
    //if를 사용해서 1,2,3번을 성공한 경우 isovercame이 true. 실패할 경우 false.

    //true일 경우 게임 계속 진행. 
    //false일 경우 리스폰. 처음 장소(2.15,-8.38,0)에서 부활.-> player.position=Vector3(2.15,-8.38,0)
    //가시에 닿을 경우 리스폰. 처음 장소(2.15,-8.38,0)에서 부활.-> player.position=Vector3(2.15,-8.38,0)

    //private float StunTime = 15f;                    //마비까지 걸리는 시간
    private float RootedTime = 3f;                   //마비가 진행되는 최대 시간. 시간안에 못 풀시 하트 한칸 감소.
    public float blinkTime = 0f;

    public int ranint;
    public bool isInRootedCoroutine;
    public bool isDoorActive = false;
    public bool isovercame;                         //'스턴을 풀 조건'(버튼 연타 조건)을 충족했는지에 대한 bool형 변수
    public bool isGameClear = false;
    //public Text StunTimeText;
    public Text RootedTimeText;

    public GameObject Player;                       //플레이어
    public GameObject Alarm;
    public GameObject Trapdoor;
    public GameObject firstfloorPlatform;
    public GameObject anotherfloorPlatform;
    public GameObject firstfloorThorn;
    public GameObject anotherfirstfloorThorn;

    public GameObject lever1, lever2, lever3, lever4;                       //웅크린자 1, 2, 3, 4
    public GameObject lever1r, lever2r, lever3r, lever4r;                   //웅크린자 변형.

    public bool touchedLever1 = false;                    //trigger인 웅크린자들을 player가 지나가면 true가 되고, 이때 버튼 누르면 SetActive가 false;
    public bool touchedLever2 = false;
    public bool touchedLever3 = false;
    public bool touchedLever4 = false;
    public bool touchedDoor = false;

    //public GameObject testLever, testLeverr;
    //public bool touchedTestLever = false;

    public Button RightBtn, LeftBtn, JumpBtn, InteractionBtn;

    Rigidbody2D rigid;

    public FadeControl fadecontrol;

    //상수 지정. 수정 필요 시 여기서 하면 됨!
    public Vector3 respawnPosition = new Vector3(2.15f, -6.76f, 0f);
    public Vector3 firstFloorPosition = new Vector3(-49.39f, -31.62f, 0f);
    public float MinimalY = -50.29f;
    public float noCheatY = -20.5f;

    private void Start()
    {
        Trapdoor.SetActive(true);
        rigid = Player.GetComponent<Rigidbody2D>();
        //StartCoroutine("StunTimer");
    }
    void Update()
    {
        blinkWhileRooted();  
        
        dontCheat();
        felloff();
        destroyStacks();
        makeAlarmActive();
        
        gameClear();
        //gameOver();
    }

    /*IEnumerator StunTimer() //스턴까지 남은 시간 잰다.
    {
        while (true)
        {
            StunTime -= 0.5f; // 시간은 계속 깎입니다.
            Debug.Log("스턴까지 남은 시간 출력: " + (int)StunTime);
            StunTimeText.text = "TIME: " + (int)StunTime; //화면 텍스트 표시
            if (StunTime <= 0)
            {
                StunTime = 15;
                StunTimeText.text = "!!";
                StartCoroutine("RootedTimer");
                break;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }*/

    public IEnumerator RootedTimer() //스턴시키는 시간 잰다.
    {
        isInRootedCoroutine = true;                                  //버튼 카운터를 활성화하기 위한 조건.
        RootedTimeText.color = new Color(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
        rigid.velocity = Vector2.zero;                               //움직임 불가.

        ranint = Random.Range(1, 4);                                               //타이머 시작 이후 밑에 숨어있던 타이머가 게이머에게 보임.
        while (true)
        {
            RootedTime -= Time.deltaTime;                                
            Debug.Log("제한 시간을 출력합니다: " + (int)RootedTime);
            RootedTimeText.text = "" + (int)RootedTime;

            didOvercame(ranint);

            if (RootedTime <= 0)
            {
                RootedTimeText.color = new Color(255 / 255f, 0 / 255f, 0 / 255f, 0 / 255f);
                RightBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
                LeftBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
                JumpBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
                InteractionBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f); //rooted 종료 후, 버튼의 알파값 원래대로 초기화

                if (isovercame)                                              //탈출 조건을 만족할 시
                {
                    Debug.Log("rooted.overcame");
                    Btnclick.RightBtnClickCounter = 0;                       //초기화해주기
                    Btnclick.LeftBtnClickCounter = 0;
                    Btnclick.JumpBtnClickCounter = 0;
                    Btnclick.InteractionBtnClickCounter = 0;
                    Debug.Log("BtnClickCounter 초기화");
                    RootedTime = 3;
                    isInRootedCoroutine = false;                             //이 시간이 다 되면 카운팅을 할 이유가 없다.
                    //StartCoroutine(StunTimer());                             //탈출.바로 StunTimer 코루틴 시작;
                    break;
                }
                else
                {
                    Debug.Log("rooted.~overcame");
                    Btnclick.RightBtnClickCounter = 0;                       //초기화해주기
                    Btnclick.LeftBtnClickCounter = 0;
                    Btnclick.JumpBtnClickCounter = 0;
                    Btnclick.InteractionBtnClickCounter = 0;
                    Debug.Log("BtnClickCounter 초기화");
                    isInRootedCoroutine = false;
                    respawn();                                               //못할 시 리스폰 위치에서 리스폰.
                    //StartCoroutine(StunTimer());                             //StunTimer 코루틴 시작.
                    break;
                }
            }
            else
                //yield return new WaitForSeconds(0.5f);
                yield return null;
            //부울변수 둔게 true면 startcoroutine stuntimer
        }
        
    }
    public void didOvercame(int ranint)
    {

        switch (ranint)
        {
            case 1:               
                if (Btnclick.LeftBtnClickCounter + Btnclick.RightBtnClickCounter >= 10)
                    isovercame = true;
                else
                    isovercame = false;
                break;
 
            case 2:               
                if (Btnclick.JumpBtnClickCounter >= 10)
                    isovercame = true;
                else
                    isovercame = false;
                break;
 
            case 3:              
                if (Btnclick.InteractionBtnClickCounter >= 10)
                    isovercame = true;
                else
                    isovercame = false;
                break;

            default:
                break;
        }

    }

    public void respawn()
    {
        fadecontrol.Fade();                             //화면 페이드아웃 -> 페이드인
        Player.transform.position = respawnPosition;    //Player 위치 초기화
        //HPManager.hp -= 1;
        
        lever1r.SetActive(false);
        lever1.SetActive(true);
        lever2r.SetActive(false);
        lever2.SetActive(true);
        lever3r.SetActive(false);
        lever3.SetActive(true);
        lever4r.SetActive(false);
        lever4.SetActive(true);                        //lever1,2,3,4 상태 초기화
        isDoorActive = false;                           //문 상태 초기화
        Trapdoor.SetActive(true);                       //트랩도어 상태 초기화
        firstfloorPlatform.SetActive(true);
        firstfloorThorn.SetActive(true);
        anotherfloorPlatform.SetActive(false);
        anotherfirstfloorThorn.SetActive(false);
        //올 초기화. 필요할까?

        //StunTime = 16.5f;                                 //화면 꺼지는 시간 고려, 15초로 조정
        //Debug.Log("stuntime 초기화.");
        RootedTime = 3f;
        Debug.Log("rootedtime 초기화.");
        Debug.Log("상태가 모두 초기화되었습니다.");
    }

    public void blinkWhileRooted()
    {
        if (isInRootedCoroutine)
        {
            

            if (blinkTime <= 0.2f)
            {
                if (ranint == 1)
                {
                    RightBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 200 / 255f);
                    LeftBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 200 / 255f);
                }
                if (ranint == 2)
                {
                    JumpBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 200 / 255f);

                }
                if (ranint == 3)
                {
                    InteractionBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 200 / 255f);
                }
            }
            else if (blinkTime > 0.2f && blinkTime < 0.4f)
            {
                if (ranint == 1)
                {
                    RightBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
                    LeftBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
                }
                if (ranint == 2)
                {
                    JumpBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);

                }
                if (ranint == 3)
                {
                    InteractionBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
                }
            }
            if (blinkTime >= 0.4f)
                blinkTime = 0f;

            blinkTime += Time.deltaTime;
        }
    }

    public void felloff() //맨 밑의 아래로 떨어졌을 때.(맵 이탈)
    {
        if (Player.transform.position.y < MinimalY)
        {
            respawn();
            Debug.Log("떨어졌어요...");
        }     
    }

    public void dontCheat() //위층 문 안열고서 아래층으로 가서 날먹하려 할 때
    {
        if (!isDoorActive)
        {
            if (Player.transform.position.y < noCheatY)
            {
                respawn();
                Debug.Log("dontcheat. 2층의 문을 열고 진행해주세요.");
            }
                
        }
    }

    public void destroyStacks()
    {
        if (!lever3.activeSelf)
        {
            firstfloorPlatform.SetActive(false);
            firstfloorThorn.SetActive(false);
            anotherfloorPlatform.SetActive(true);
            anotherfirstfloorThorn.SetActive(true);
            Debug.Log("destroyed Stacks");
        }
    }

    public void makeAlarmActive()
    {
        if (!lever1.activeSelf &&
            !lever2.activeSelf &&
            !lever3.activeSelf &&
            !lever4.activeSelf)
        {
            Trapdoor.SetActive(false);
            Debug.Log("알람시계가 활성화되었습니다.");
        }
    }

    public void gameClear()
    {
        if (isGameClear)
        {
            //ㅇㅋ 해놓다
            StopAllCoroutines();
            rigid.velocity = Vector2.zero;
            rigid.gravityScale = 0f;
            Debug.Log("다음 꿈으로 빠집니다...");
            LoadManagerSH.singleTon.GameEnd();
             //메인 프로그래머님 다음 장면으로 연결해주시면 됩니다.
        }
            
    }

    /*public void gameOver()
    {
        if (HPManager.hp == 0)
        {
            //페이드 아웃. 점프스케어도 할거면 한다.
            //메인 프로그래머님 다음 장면으로 연결해주시면 됩니다.
            //ㅇㅋ 해놓다
            StopAllCoroutines();
            rigid.velocity = Vector2.zero;
            rigid.gravityScale = 0f;
            Debug.Log("DEAD!");
            LoadManagerSH.singleTon.GameEnd();
            
        }
    }*/

    /*(public void OnGameExit()
    {
        LoadManagerSH.singleTon.GameEnd();
    }*/
}