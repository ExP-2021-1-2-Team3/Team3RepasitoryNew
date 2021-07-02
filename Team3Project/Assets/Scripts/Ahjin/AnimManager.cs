using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public static Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

}
