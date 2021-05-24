using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float maxRate = 10f;
    public float minRate = 5f;
    public PhaseManager phaseManager;
    

    private float rate;
    private float timeAfterSpawn;
    void Start()
    {
        rate = Random.Range(minRate, maxRate);
        timeAfterSpawn = 0f;
    }

    void Update()
    {
        timeAfterSpawn += Time.deltaTime;
        if (timeAfterSpawn > rate)
        {
            timeAfterSpawn = 0f;
            rate = Random.Range(minRate, maxRate);
            GameObject obs = Instantiate(prefab, transform.position, transform.rotation);
            obs.SetActive(true);
        }

        if (phaseManager.phase == 0)
        {

        }
    }
}