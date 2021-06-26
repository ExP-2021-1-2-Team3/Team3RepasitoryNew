using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject hand, h1, h2, h3, h4, h5, h6, h7, clock;
    public float maxRate = 15f;
    public float minRate = 3f;
    public PhaseManager phaseManager;
    public int rotateSpeed;



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
            h1.gameObject.SetActive(false);
            h2.gameObject.SetActive(false);
            h3.gameObject.SetActive(false);
            h4.gameObject.SetActive(false);
            h5.gameObject.SetActive(false);
            h6.gameObject.SetActive(false);
            h7.gameObject.SetActive(false);
            clock.gameObject.SetActive(false);
        }

        if (phaseManager.phase == 1)
        {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(true);
            h4.gameObject.SetActive(false);
            h5.gameObject.SetActive(false);
            h6.gameObject.SetActive(false);
            h7.gameObject.SetActive(false);
            clock.gameObject.SetActive(false);
        }

        if (phaseManager.phase == 2)
        {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(true);
            h4.gameObject.SetActive(true);
            h5.gameObject.SetActive(true);
            h6.gameObject.SetActive(false);
            h7.gameObject.SetActive(false);
            clock.gameObject.SetActive(false);
        }
        
        if (phaseManager.phase == 3)
        {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(true);
            h4.gameObject.SetActive(true);
            h5.gameObject.SetActive(true);
            h6.gameObject.SetActive(true);
            h7.gameObject.SetActive(true);
            clock.gameObject.SetActive(false);
        }

        if (phaseManager.phase == 4)
        {
            hand.gameObject.SetActive(false);
            clock.gameObject.SetActive(true);
        }
    }
}