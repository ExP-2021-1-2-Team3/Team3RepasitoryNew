using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blanket_Carpet : MonoBehaviour
{
    public GameObject touchedObj;
    public GameObject Blanket;
    public GameObject Carpet;
    RaycastHit2D hit;
    public Camera cam;
    public bool isBlanketOpened = false;
    public bool isCarpetOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CloseBtn.isOpenedUI)
            {
                return;
            }
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                touchedObj = hit.collider.gameObject;
            }
        }

        if (touchedObj == Blanket)
        {
            if (isCarpetOpened)
            {
                Debug.Log("이불을 젖혔더니 책이 나왔다!");
                isBlanketOpened = true;
                cam.transform.position = new Vector3(0, -45, -10);
            }
            else
            {
                Debug.Log("이불을 젖혔더니 책이 나왔다!");
                isBlanketOpened = true;
                cam.transform.position = new Vector3(0, -15, -10);
            }
            touchedObj = null;
        }
        if (touchedObj == Carpet)
        {
            if (isBlanketOpened)
            {
                Debug.Log("카페트를 들었더니 앨범이 나왔다!");
                isCarpetOpened = true;
                cam.transform.position = new Vector3(0, -45, -10);
            }
            else
            {
                Debug.Log("카페트를 들었더니 앨범이 나왔다!");
                isCarpetOpened = true;
                cam.transform.position = new Vector3(0, -30, -10);
            }
            touchedObj = null;
        }
    }
}
