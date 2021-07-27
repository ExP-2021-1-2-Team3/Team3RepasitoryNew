using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerJH : MonoBehaviour
{
    //1. ���ϱ��� �ɸ��� �ð��� ī��Ʈ�� �Ѵ�.
    //2. ���� �������� �����Ѵ�.
    //3. ü�¿� ������ ��ġ�� ������ �����Ѵ�.
    //4. ���� Ŭ����, ���� ������ �����Ѵ�.

    //ī��Ʈ�ٿ��� ������
    //�÷��̾��� ������(����)�� �ִ� 5�ʵ��� 0���� �����.-> player.rigid.velocity=Vector3.zero;

    //�׵���
    //case 1. LeftBtn, RightBtn �� �����Ƽ� 15�� Ŭ���ؾ� ��������.
    //case 2. JumpBtn�� 20�� Ŭ���ؾ� ��������.
    //case 3. ��ȣ�ۿ�Btn�� 20�� Ŭ���ؾ� ��������. 
    //if�� ����ؼ� 1,2,3���� ������ ��� isovercame�� true. ������ ��� false.

    //true�� ��� ���� ��� ����. 
    //false�� ��� ������. ó�� ���(2.15,-8.38,0)���� ��Ȱ.-> player.position=Vector3(2.15,-8.38,0)
    //���ÿ� ���� ��� ������. ó�� ���(2.15,-8.38,0)���� ��Ȱ.-> player.position=Vector3(2.15,-8.38,0)

    private float StunTime = 15f;                    //������� �ɸ��� �ð�
    private float RootedTime = 5f;                   //���� ����Ǵ� �ִ� �ð�. �ð��ȿ� �� Ǯ�� ��Ʈ ��ĭ ����.
    public bool isInRootedCoroutine;
    public bool isDoorActive = false;
    public bool isovercame;                         //'������ Ǯ ����'(��ư ��Ÿ ����)�� �����ߴ����� ���� bool�� ����
    public bool isGameClear = false;
    public Text StunTimeText;
    public Text RootedTimeText;

    public GameObject Player;                       //�÷��̾�
    public GameObject Alarm;
    public GameObject Trapdoor;
    public GameObject firstfloorPlatform;
    public GameObject anotherfloorPlatform;
    public GameObject firstfloorThorn;
    public GameObject anotherfirstfloorThorn;

    public GameObject Curled1, Curled2, Curled3, Curled4;                       //��ũ���� 1, 2, 3, 4
    public GameObject Curled1r, Curled2r, Curled3r, Curled4r;                   //��ũ���� ����.
    public bool touchedC1 = false;                    //trigger�� ��ũ���ڵ��� player�� �������� true�� �ǰ�, �̶� ��ư ������ SetActive�� false;
    public bool touchedC2 = false;
    public bool touchedC3 = false;
    public bool touchedC4 = false;
    public bool touchedDoor = false;

    public Button RightBtn, LeftBtn, JumpBtn, InteractionBtn;

    Rigidbody2D rigid;

    public FadeControl fadecontrol;

    //��� ����. ���� �ʿ� �� ���⼭ �ϸ� ��!
    public Vector3 respawnPosition = new Vector3(2.15f, -6.76f, 0f);
    public Vector3 firstFloorPosition = new Vector3(-49.39f, -31.62f, 0f);
    public float MinimalY = -50.29f;
    public float noCheatY = -20.5f;

    private void Start()
    {
        Trapdoor.SetActive(true);
        rigid = Player.GetComponent<Rigidbody2D>();
        StartCoroutine("StunTimer");
    }
    void Update()
    {
        dontCheat();
        felloff();
        destroyStacks();
        makeAlarmActive();
        
        gameClear();
        gameOver();
    }
    IEnumerator StunTimer() //���ϱ��� ���� �ð� ���.
    {
        while (true)
        {
            StunTime -= 0.5f; // �ð��� ��� ���Դϴ�.
            Debug.Log("���ϱ��� ���� �ð� ���: " + (int)StunTime);
            StunTimeText.text = "TIME: " + (int)StunTime; //ȭ�� �ؽ�Ʈ ǥ��
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
    }
    IEnumerator RootedTimer() //���Ͻ�Ű�� �ð� ���.
    {
        isInRootedCoroutine = true;                                  //��ư ī���͸� Ȱ��ȭ�ϱ� ���� ����.
        RootedTimeText.color = new Color(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
        rigid.velocity = Vector2.zero;                               //������ �Ұ�.

        int ranint = Random.Range(1, 4);                                               //Ÿ�̸� ���� ���� �ؿ� �����ִ� Ÿ�̸Ӱ� ���̸ӿ��� ����.
        while (true)
        {
            RootedTime -= 0.5f;                                //StunTime�� �� ������ ���� RootedTimer�� �ð��� ���Դϴ�.
            Debug.Log("���� �ð��� ����մϴ�: " + (int)RootedTime);
            RootedTimeText.text = "" + (int)RootedTime;

            didOvercame(ranint);

            if (RootedTime <= 0)
            {
                RootedTimeText.color = new Color(255 / 255f, 0 / 255f, 0 / 255f, 0 / 255f);
                RightBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
                LeftBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
                JumpBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
                InteractionBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f); //rooted ���� ��, ��ư�� ���İ� ������� �ʱ�ȭ

                if (isovercame)                                              //Ż�� ������ ������ ��
                {
                    Debug.Log("rooted.overcame");
                    Btnclick.RightBtnClickCounter = 0;                       //�ʱ�ȭ���ֱ�
                    Debug.Log("RightBtnClickCounter �ʱ�ȭ");
                    Btnclick.LeftBtnClickCounter = 0;
                    Debug.Log("LeftBtnClickCounter �ʱ�ȭ");
                    Btnclick.JumpBtnClickCounter = 0;
                    Debug.Log("JumpBtnClickCounter �ʱ�ȭ");
                    Btnclick.InteractionBtnClickCounter = 0;
                    Debug.Log("InteractionBtnClickCounter �ʱ�ȭ");
                    RootedTime = 5;
                    isInRootedCoroutine = false;                             //�� �ð��� �� �Ǹ� ī������ �� ������ ����.
                    StartCoroutine(StunTimer());                             //Ż��.�ٷ� StunTimer �ڷ�ƾ ����;
                    break;
                }
                else
                {
                    Debug.Log("rooted.~overcame");
                    Btnclick.RightBtnClickCounter = 0;                       //�ʱ�ȭ���ֱ�
                    Debug.Log("RightBtnClickCounter �ʱ�ȭ");
                    Btnclick.LeftBtnClickCounter = 0;
                    Debug.Log("LeftBtnClickCounter �ʱ�ȭ");
                    Btnclick.JumpBtnClickCounter = 0;
                    Debug.Log("JumpBtnClickCounter �ʱ�ȭ");
                    Btnclick.InteractionBtnClickCounter = 0;
                    Debug.Log("InteractionBtnClickCounter �ʱ�ȭ");
                    isInRootedCoroutine = false;
                    respawn();                                               //���� �� ������ ��ġ���� ������.
                    StartCoroutine(StunTimer());                             //StunTimer �ڷ�ƾ ����.
                    break;
                }
            }
            else
                yield return new WaitForSeconds(0.5f);
            //�οﺯ�� �а� true�� startcoroutine stuntimer
        }
        
    }
    public void didOvercame(int ranint)
    {

        switch (ranint)
        {
            case 1:
                RightBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 200 / 255f); //��Ϳ� �ش��� ������ �ش� ���̽����� ������ �ϴ� ��ư�� ���İ��� ���.
                LeftBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 200 / 255f);  //�� �ľ� �ϴ����� �ν��� ���Ѿ� �ϱ� �����̴�.
                if (Btnclick.LeftBtnClickCounter + Btnclick.RightBtnClickCounter >= 30)
                    isovercame = true;
                else
                    isovercame = false;
                break;
            case 2:
                JumpBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 200 / 255f);
                if (Btnclick.JumpBtnClickCounter >= 20)
                    isovercame = true;
                else
                    isovercame = false;
                break;
            case 3:
                InteractionBtn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 200 / 255f);
                if (Btnclick.InteractionBtnClickCounter >= 20)
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
        fadecontrol.Fade();                             //ȭ�� ���̵�ƿ� -> ���̵���
        Player.transform.position = respawnPosition;    //Player ��ġ �ʱ�ȭ
        HPManager.hp -= 1;
        
        Curled1r.SetActive(false);
        Curled1.SetActive(true);
        Curled2r.SetActive(false);
        Curled2.SetActive(true);
        Curled3r.SetActive(false);
        Curled3.SetActive(true);
        Curled4r.SetActive(false);
        Curled4.SetActive(true);                        //Curled1,2,3,4 ���� �ʱ�ȭ
        isDoorActive = false;                           //�� ���� �ʱ�ȭ
        Trapdoor.SetActive(true);                       //Ʈ������ ���� �ʱ�ȭ
        firstfloorPlatform.SetActive(true);
        firstfloorThorn.SetActive(true);
        anotherfloorPlatform.SetActive(false);
        anotherfirstfloorThorn.SetActive(false);
        //�� �ʱ�ȭ. �ʿ��ұ�?

        StunTime = 16.5f;                                 //ȭ�� ������ �ð� ���, 15�ʷ� ����
        Debug.Log("stuntime �ʱ�ȭ.");
        RootedTime = 5f;
        Debug.Log("rootedtime �ʱ�ȭ.");
        Debug.Log("���°� ��� �ʱ�ȭ�Ǿ����ϴ�.");
    }

    public void felloff() //�� ���� �Ʒ��� �������� ��.(�� ��Ż)
    {
        if (Player.transform.position.y < MinimalY)
        {
            respawn();
            Debug.Log("���������...");
        }     
    }

    public void dontCheat() //���� �� �ȿ��� �Ʒ������� ���� �����Ϸ� �� ��
    {
        if (!isDoorActive)
        {
            if (Player.transform.position.y < noCheatY)
            {
                respawn();
                Debug.Log("dontcheat. 2���� ���� ���� �������ּ���.");
            }
                
        }
    }
    public void destroyStacks()
    {
        if (!Curled3.activeSelf)
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
        if (!Curled1.activeSelf &&
            !Curled2.activeSelf &&
            !Curled3.activeSelf &&
            !Curled4.activeSelf)
        {
            Trapdoor.SetActive(false);
            Debug.Log("�˶��ð谡 Ȱ��ȭ�Ǿ����ϴ�.");
        }
    }

    public void gameClear()
    {
        if (isGameClear)
        {
            //���� �س���
            StopAllCoroutines();
            rigid.velocity = Vector2.zero;
            rigid.gravityScale = 0f;
            Debug.Log("���� ������ �����ϴ�...");
            LoadManagerSH.singleTon.GameEnd();
             //���� ���α׷��Ӵ� ���� ������� �������ֽø� �˴ϴ�.
        }
            
    }

    public void gameOver()
    {
        if (HPManager.hp == 0)
        {
            //���̵� �ƿ�. �������ɾ �ҰŸ� �Ѵ�.
            //���� ���α׷��Ӵ� ���� ������� �������ֽø� �˴ϴ�.
            //���� �س���
            StopAllCoroutines();
            rigid.velocity = Vector2.zero;
            rigid.gravityScale = 0f;
            Debug.Log("DEAD!");
            LoadManagerSH.singleTon.GameEnd();
            
        }
    }

    /*(public void OnGameExit()
    {
        LoadManagerSH.singleTon.GameEnd();
    }*/
}