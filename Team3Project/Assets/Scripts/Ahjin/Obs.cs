using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obs : MonoBehaviour
{
    Vector3 p1;
    Vector3 p2;
    float t = 0;
    public Hp hp;
    

    void Start()
    {
        p1 = transform.position;
        p2 = new Vector3(0, 0, 0);
        Vector3 myPosition = new Vector3(0, 0, 0);
        transform.position = myPosition;
    }

    void Update()
    {
        if (t < 1)
        {
            t += Time.deltaTime * 0.5f;
        }
        else if (t >= 1)
        {
            t = 1;
        }

        transform.position = (1 - t) * p1 + t * p2;

        if (transform.position.sqrMagnitude < 2)
        {
            Hp.health -= 1;
            gameObject.SetActive(false);
        }

        if (Hp.health == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
