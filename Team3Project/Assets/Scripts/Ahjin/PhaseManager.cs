using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    float phaseTimer = 0;
    public int phase = 0;
    void Update()
    {
        phaseTimer += Time.deltaTime;
        if (phaseTimer > 1)
        {
            phase = phase + 1;
            phaseTimer = 0;
        }

    }
}
