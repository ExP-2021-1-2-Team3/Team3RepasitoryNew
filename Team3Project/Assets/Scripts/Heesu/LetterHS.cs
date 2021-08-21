using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//화면에서 빗발치는 글자 오브젝트
public class LetterHS : MonoBehaviour
{
    //Inspector에서 this_letter값을 보실 수 있습니다.
    [Header("Set In inspector")]
    public float gravity;

    //파티클을 끄고 싶으시다면 fasle를 넣어주세요.
    public static bool enable_particle = false;

    [Header("Set In Runtime")]
    public char this_letter;
    private const float destroyLimit = -15.0f;
    private bool game_is_not_over = true;

    private Rigidbody2D rigidBody;
    private BoxCollider2D colision;
    private ParticleSystem particle;
    private SpriteRenderer image;

    private bool is_animating = false;
    private static char prev_char = '?';

    //다시 화면에 등장할 때 불리는 함수
    public void ResetByChar(char setChar)
    {
        //클릭이 전에 된 개체가 처리가 끝나지 않은채로 바로 초기화하는 일은 방지.
        if(is_animating && enable_particle)
            return;
        
        //텍스트 매쉬 하고 리지드 바디 가져오기
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        particle = gameObject.GetComponent<ParticleSystem>();
        colision = gameObject.GetComponent<BoxCollider2D>();
        image = gameObject.GetComponent<SpriteRenderer>();

        if(enable_particle){
            //파티클 시스템, 콜리전, 이미지 활성화            
            particle.Play(true);
            colision.enabled = true;
            image.color = Color.white;
            rigidBody.gravityScale = gravity;
        }

        //랜덤 위치 / 기울기 선정
        gameObject.transform.position = new Vector3(Random.Range(-7.5f, 7.5f), 9, 0);
        rigidBody.SetRotation(Random.Range(-30, 30));

        //각 알파벳 별 맞는 스프라이트 할당
        image.sprite = Resources.Load<Sprite>(setChar.ToString()) as Sprite;
        this_letter = setChar;
        gameObject.SetActive(true);
        
        if(!enable_particle)
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    //이걸 이용하면 사용자가 글자를 클릭했는지 알 수 있음.
    void OnMouseDown()
    {
        if(game_is_not_over){
            if(prev_char == 'X' && this_letter == 'X')
                enable_particle = true;
            prev_char = this_letter;
            LetterUiManagerHS.GetInstance().InputChar(this_letter);
            if(enable_particle)
                StartCoroutine(Fade());
            else
                gameObject.SetActive(false);
        }
    }

    public void DisableTouchEvent(){
        game_is_not_over = false;
    }

    //플레이어가 볼 수 없는 범위로 나가버렸는가?
    //나중에 Trigger 쓸 예정...
    private bool IsOutOfRange()
    {
        return (transform.localPosition.y < destroyLimit);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!enable_particle)
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    // Update is called once per frame
    void Update()
    {
        //현시점에서는 그다지 필요없는 코드
        //어짜피 리스트 안에서 순회당하면서 다시 불려옴
        if(this.IsOutOfRange()){
            if(enable_particle)
                StartCoroutine(Fade());
            else
                gameObject.SetActive(false);
        }
    }

    IEnumerator Fade(){
        is_animating = true;
        WaitForEndOfFrame delay = new WaitForEndOfFrame();
        colision.enabled = false;
        //rigidBody.velocity = Vector2.zero;
        //rigidBody.gravityScale = 0.0f;
        particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        while(particle.particleCount > 0){
            yield return delay;
        }

        gameObject.SetActive(false);
        is_animating = false;
    }
}
