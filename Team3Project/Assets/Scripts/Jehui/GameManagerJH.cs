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

    //private float StunTime = 15f;                    //������� �ɸ��� �ð�
    private float RootedTime = 3f;                   //���� ����Ǵ� �ִ� �ð�. �ð��ȿ� �� Ǯ�� ��Ʈ ��ĭ ����.
    public float blinkTime = 0f;

    public int ranint;
    public bool isInRootedCoroutine;
    public bool isDoorActive = false;
    public bool isovercame;                         //'������ Ǯ ����'(��ư ��Ÿ ����)�� �����ߴ����� ���� bool�� ����
    public bool isGameClear = false;
    //public Text StunTimeText;
    public Text RootedTimeText;

    public GameObject Player;                       //�÷��̾�
    public GameObject Alarm;
    public GameObject Trapdoor;
    public GameObject firstfloorPlatform;
    public GameObject anotherfloorPlatform;
    public GameObject firstfloorThorn;
    public GameObject anotherfirstfloorThorn;

    public GameObject lever1, lever2, lever3, lever4;                       //��ũ���� 1, 2, 3, 4
    public GameObject lever1r, lever2r, lever3r, lever4r;                   //��ũ���� ����.

    public bool touchedLever1 = false;                    //trigger�� ��ũ���ڵ��� player�� �������� true�� �ǰ�, �̶� ��ư ������ SetActive�� false;
    public bool touchedLever2 = false;
    public bool touchedLever3 = false;
    public bool touchedLever4 = false;
    public bool touchedDoor = false;

    //public GameObject testLever, testLeverr;
    //public bool touchedTestLever = false;

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

    /*IEnumerator StunTimer() //���ϱ��� ���� �ð� ���.
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
    }*/

    public IEnumerator RootedTimer() //���Ͻ�Ű�� �ð� ���.
    {
        isInRootedCoroutine = true;                                  //��ư ī���͸� Ȱ��ȭ�ϱ� ���� ����.
        RootedTimeText.color = new Color(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
        rigid.velocity = Vector2.zero;                               //������ �Ұ�.

        ranint = Random.Range(1, 4);                                               //Ÿ�̸� ���� ���� �ؿ� �����ִ� Ÿ�̸Ӱ� ���̸ӿ��� ����.
        while (true)
        {
            RootedTime -= Time.deltaTime;                                
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
                    Btnclick.LeftBtnClickCounter = 0;
                    Btnclick.JumpBtnClickCounter = 0;
                    Btnclick.InteractionBtnClickCounter = 0;
                    Debug.Log("BtnClickCounter �ʱ�ȭ");
                    RootedTime = 3;
                    isInRootedCoroutine = false;                             //�� �ð��� �� �Ǹ� ī������ �� ������ ����.
                    //StartCoroutine(StunTimer());                             //Ż��.�ٷ� StunTimer �ڷ�ƾ ����;
                    break;
                }
                else
                {
                    Debug.Log("rooted.~overcame");
                    Btnclick.RightBtnClickCounter = 0;                       //�ʱ�ȭ���ֱ�
                    Btnclick.LeftBtnClickCounter = 0;
                    Btnclick.JumpBtnClickCounter = 0;
                    Btnclick.InteractionBtnClickCounter = 0;
                    Debug.Log("BtnClickCounter �ʱ�ȭ");
                    isInRootedCoroutine = false;
                    respawn();                                               //���� �� ������ ��ġ���� ������.
                    //StartCoroutine(StunTimer());                             //StunTimer �ڷ�ƾ ����.
                    break;
                }
            }
            else
                //yield return new WaitForSeconds(0.5f);
                yield return null;
            //�οﺯ�� �а� true�� startcoroutine stuntimer
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
        fadecontrol.Fade();                             //ȭ�� ���̵�ƿ� -> ���̵���
        Player.transform.position = respawnPosition;    //Player ��ġ �ʱ�ȭ
        //HPManager.hp -= 1;
        
        lever1r.SetActive(false);
        lever1.SetActive(true);
        lever2r.SetActive(false);
        lever2.SetActive(true);
        lever3r.SetActive(false);
        lever3.SetActive(true);
        lever4r.SetActive(false);
        lever4.SetActive(true);                        //lever1,2,3,4 ���� �ʱ�ȭ
        isDoorActive = false;                           //�� ���� �ʱ�ȭ
        Trapdoor.SetActive(true);                       //Ʈ������ ���� �ʱ�ȭ
        firstfloorPlatform.SetActive(true);
        firstfloorThorn.SetActive(true);
        anotherfloorPlatform.SetActive(false);
        anotherfirstfloorThorn.SetActive(false);
        //�� �ʱ�ȭ. �ʿ��ұ�?

        //StunTime = 16.5f;                                 //ȭ�� ������ �ð� ���, 15�ʷ� ����
        //Debug.Log("stuntime �ʱ�ȭ.");
        RootedTime = 3f;
        Debug.Log("rootedtime �ʱ�ȭ.");
        Debug.Log("���°� ��� �ʱ�ȭ�Ǿ����ϴ�.");
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

    /*public void gameOver()
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
    }*/

    /*(public void OnGameExit()
    {
        LoadManagerSH.singleTon.GameEnd();
    }*/
}