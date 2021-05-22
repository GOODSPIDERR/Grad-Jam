using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowHit : MonoBehaviour
{
    private Animator pillowAnim;

    void Start()
    {
        pillowAnim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        
    }
}
