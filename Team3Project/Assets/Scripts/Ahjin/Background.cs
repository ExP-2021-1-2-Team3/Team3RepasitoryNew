using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public PhaseManager phaseManager;
    public GameObject bg1, bg2, bg3, bg4;

    void Update()
    {
        if (phaseManager.phase == 0)
        {
            bg1.gameObject.SetActive(true);
            bg2.gameObject.SetActive(false);
            bg3.gameObject.SetActive(false);
            bg4.gameObject.SetActive(false);
        }

        if (phaseManager.phase == 1)
        {
            bg1.gameObject.SetActive(false);
            bg2.gameObject.SetActive(true);
            bg3.gameObject.SetActive(false);
            bg4.gameObject.SetActive(false);
        }

        if (phaseManager.phase == 2)
        {
            bg1.gameObject.SetActive(false);
            bg2.gameObject.SetActive(false);
            bg3.gameObject.SetActive(true);
            bg4.gameObject.SetActive(false);
        }

        if (phaseManager.phase == 3)
        {
            bg1.gameObject.SetActive(false);
            bg2.gameObject.SetActive(false);
            bg3.gameObject.SetActive(false);
            bg4.gameObject.SetActive(true);
        }

        if (phaseManager.phase == 4)
        {
            bg1.gameObject.SetActive(false);
            bg2.gameObject.SetActive(false);
            bg3.gameObject.SetActive(false);
            bg4.gameObject.SetActive(true);
        }
    }
}