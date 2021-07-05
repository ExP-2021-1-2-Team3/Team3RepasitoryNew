using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//화면에서 빗발치는 글자 오브젝트
public class LetterHS : MonoBehaviour
{
    //Inspector에서 this_letter값을 보실 수 있습니다.
    [Header("Set In Runtime")]
    public char this_letter;
    private const float destroyLimit = -15.0f;
    private bool game_is_not_over = true;

    //다시 화면에 등장할 때 불리는 함수
    public void ResetByChar(char setChar)
    {
        //텍스트 매쉬 하고 리지드 바디 가져오기
        Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer image = gameObject.GetComponent<SpriteRenderer>();

        //랜덤 위치 / 기울기 선정
        gameObject.transform.position = new Vector3(Random.Range(-7.5f, 7.5f), 9, 0);
        rigidBody.SetRotation(Random.Range(-30, 30));

        //각 알파벳 별 맞는 스프라이트 할당
        image.sprite = Resources.Load<Sprite>(setChar.ToString()) as Sprite;
        this_letter = setChar;
        gameObject.SetActive(true);
    }

    //이걸 이용하면 사용자가 글자를 클릭했는지 알 수 있음.
    void OnMouseDown()
    {
        if(game_is_not_over){
            LetterUiManagerHS.GetInstance().InputChar(this_letter);
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
    }

    // Update is called once per frame
    void Update()
    {
        //현시점에서는 그다지 필요없는 코드
        //어짜피 리스트 안에서 순회당하면서 다시 불려옴
        if(this.IsOutOfRange()){
            gameObject.SetActive(false);
        }
    }
}
