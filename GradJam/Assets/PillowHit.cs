using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowHit : MonoBehaviour
{
    private Animator pillowAnim;
    public bool leftHit = true;

    void Start()
    {
        pillowAnim = gameObject.GetComponent<Animator>();
    }
    public void EndSwingLeft()
    {
        pillowAnim.SetBool("swingLeftBool", false);
        //print("left done");

    }
    public void EndSwingRight()
    {

        pillowAnim.SetBool("swingRightBool", false);
        //print("right done");

    }
    void Update()
    {
        
        
        if (Input.GetMouseButtonDown(0))
        {
            if (leftHit)
            {
                //pillowAnim.SetBool("swingLeftBool", false);
                pillowAnim.SetBool("swingLeftBool", true);
                //print("time to swing right");
                leftHit = false;

            }
            else
            {
                //pillowAnim.SetBool("swingRightBool", false);
                pillowAnim.SetBool("swingRightBool", true);
                //print("time to swing left");
                leftHit = true;

            }
            
            //pillowAnim.SetBool("startBlendTree", true);


        }
    }
}
