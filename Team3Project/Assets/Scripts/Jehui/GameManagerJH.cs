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

    private float StunTime = 15;                    //������� �ɸ��� �ð�
    private float RootedTime = 5;                   //���� ����Ǵ� �ִ� �ð�. �ð��ȿ� �� Ǯ�� ��Ʈ ��ĭ ����.
    public bool isInRootedCoroutine;
    public bool isDoorActive = false;
    public bool isovercame;                         //'������ Ǯ ����'(��ư ��Ÿ ����)�� �����ߴ����� ���� bool�� ����
    public bool isGameClear = false;
    public Text StunTimeText;
    public Text RootedTimeText;
 
    public GameObject Player;                       //�÷��̾�
    public GameObject Alarm;
    public GameObject Trapdoor;

    public GameObject Curled1, Curled2, Curled3, Curled4;                       //��ũ���� 1, 2, 3, 4
    public bool touchedC1 = false;                    //trigger�� ��ũ���ڵ��� player�� �������� true�� �ǰ�, �̶� ��ư ������ SetActive�� false;
    public bool touchedC2 = false;
    public bool touchedC3 = false;
    public bool touchedC4 = false;
    public bool touchedDoor = false;
    //public SpriteRenderer Curled1sr, Curled2sr, Curled3sr, Curled4sr;  �ʿ��ұ�...? spriterenderer ������Ʈ�� ��������� �̷��� �ߴ�.(������Ʈ�� ���İ��� �����ϱ� ���ؼ�)
    
    Rigidbody2D rigid;   
   
    //��� ����. ���� �ʿ� �� ���⼭ �ϸ� ��!
    public Vector3 respawnPosition= new Vector3(2.15f, -8.38f, 0);
    public Vector3 firstFloorPosition = new Vector3(-49.39f, -33.24f, 0f);
    public float MinimalY = -50.29f;
    public float noCheatY = -20.5f;

    private void Start()
    {
        Trapdoor.SetActive(true);
        rigid = Player.GetComponent<Rigidbody2D>();
        StartCoroutine(StunTimer());      
    }
    void Update()
    {
        dontCheat();
        felloff();
        makeAlarmActive();
        gameClear();
        gameOver();
    }
    IEnumerator StunTimer() //���ϱ��� ���� �ð� ���.
    {
        while (true)
        {
            StunTime -= Time.deltaTime; // �ð��� ��� ���Դϴ�.
            Debug.Log("���ϱ��� ���� �ð� ���: " + (int)StunTime);
            StunTimeText.text = "TIME: " + (int)StunTime; //ȭ�� �ؽ�Ʈ ǥ��
            if ((int)StunTime <= 0)
            {
                StunTime = 15;
                StunTimeText.text = "!!";               
                StartCoroutine(RootedTimer());
                break;
            }
            else
            {
                yield return null;
            }
        }
    }
    IEnumerator RootedTimer() //���Ͻ�Ű�� �ð� ���.
    {
        isInRootedCoroutine = true;                                  //��ư ī���͸� Ȱ��ȭ�ϱ� ���� ����.
        Color color = RootedTimeText.color;
        rigid.velocity = Vector3.zero;                               //������ �Ұ�.
        
        color.a = 1f;                                                //Ÿ�̸� ���� ���� �ؿ� �����ִ� Ÿ�̸Ӱ� ���̸ӿ��� ����.
        while (true)
        {
            RootedTime -= Time.deltaTime;                                //StunTime�� �� ������ ���� RootedTimer�� �ð��� ���Դϴ�.
            Debug.Log("���� �ð��� ����մϴ�: " + (int)RootedTime);
            RootedTimeText.text = "" + (int)RootedTime;

            didOvercame();

            if ((int)RootedTime <= 0)
            {
                color.a = 0f;
                if (isovercame)                                              //Ż�� ������ ������ ��
                {
                    RootedTime = 5;
                    isInRootedCoroutine = false;                             //�� �ð��� �� �Ǹ� ī������ �� ������ ����.
                    StartCoroutine(StunTimer());                //Ż��.�ٷ� StunTimer �ڷ�ƾ ����;
                }
                else
                {
                    isInRootedCoroutine = false;
                    respawn();                                               //���� �� ������ ��ġ���� ������.
                    StartCoroutine(StunTimer());                //StunTimer �ڷ�ƾ ����.
                }
            }
            else
                yield return null;
        }
        
    }
    public void didOvercame()
    {        
        int ranint = Random.Range(1, 4);
        switch (ranint)
        {
            case 1:

                if (Btnclick.LeftBtnClickCounter + Btnclick.RightBtnClickCounter >= 30)
                    isovercame = true;
                else
                    isovercame = false;
                break;
            case 2:
                if(Btnclick.JumpBtnClickCounter>=20)
                    isovercame = true;
                else
                    isovercame = false;
                break;
            case 3:
                if(Btnclick.InteractionBtnClickCounter>=20)
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
        //ȭ�� ���̵�ƿ� -> ���̵��� �ڵ�;
        Player.transform.position = respawnPosition;
        HPManager.hp -= 1;
    }

    public void felloff() //�� ���� �Ʒ��� �������� ��.(�� ��Ż)
    {
        if (Player.transform.position.y < MinimalY)
            respawn();    
    }
   
    public void dontCheat() //���� �� �ȿ��� �Ʒ������� ���� �����Ϸ� �� ��
    {
        if(!isDoorActive)
        {
            if (Player.transform.position.y < noCheatY)
                respawn();
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
        }
    }

    public void gameClear()
    {
        if(isGameClear)
            Debug.Log("���� Ż���߽��ϴ�...?"); //���� ���α׷��Ӵ� ���� ������� �������ֽø� �˴ϴ�.
    }
    
    public void gameOver()
    {
        if (HPManager.hp == 0)
        {
            //���̵� �ƿ�. �������ɾ �ҰŸ� �Ѵ�.
            //���� ���α׷��Ӵ� ���� ������� �������ֽø� �˴ϴ�.
        }
    }
}
