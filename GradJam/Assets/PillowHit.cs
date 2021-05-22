using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowHit : MonoBehaviour
{
    private Animator pillowAnim;
    public bool swing = false;

    void Start()
    {
        pillowAnim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!swing) //if you swing for the first time
            {
                pillowAnim.SetBool("swingLeftBool", true);
                
            }
            else if (swing)
            {
                pillowAnim.SetBool("swingBool", false);

            }

            swing = true;

        }
    }
}
