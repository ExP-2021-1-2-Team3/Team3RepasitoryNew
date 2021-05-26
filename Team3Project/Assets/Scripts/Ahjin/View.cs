using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public PhaseManager phaseManager;
    public GameObject p1, p2, p3;

    void Start()
    {
        p1.gameObject.SetActive(false);
        p2.gameObject.SetActive(false);
        p3.gameObject.SetActive(false);
    }

    void Update()
    {
        if (phaseManager.phase == 0)
        {
            p1.gameObject.SetActive(false);
            p2.gameObject.SetActive(false);
            p3.gameObject.SetActive(false);
        }

        if (phaseManager.phase == 1)
        {
            p1.gameObject.SetActive(true);
            p2.gameObject.SetActive(false);
            p3.gameObject.SetActive(false);
        }

        if (phaseManager.phase == 2)
        {
            p1.gameObject.SetActive(false);
            p2.gameObject.SetActive(true);
            p3.gameObject.SetActive(false);
        }

        if (phaseManager.phase == 3)
        {
            p1.gameObject.SetActive(false);
            p2.gameObject.SetActive(false);
            p3.gameObject.SetActive(true);
        }
    }
}
