using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowHit : MonoBehaviour
{
    private Animator pillowAnim;
    public bool leftHit = true;
    public bool iwasThrown;


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
    public void Moving()
    {
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S)))
        {
            pillowAnim.SetBool("movingBool", true);
            //pillowAnim.SetTrigger("movementTrigger");
            //print("I'm moving");
        }
        else
        {
            pillowAnim.SetBool("movingBool", false);
        }
    }
    public void Throw()
    {
        if (Input.GetMouseButtonDown(1))
        {
            pillowAnim.SetTrigger("throwTrigger");
            print("I'm throwing");
            //iwasThrown = true;
            StartCoroutine(WaitaSecond());
        }
    }
    public void Catch()
    {
        if (iwasThrown)
        {
            pillowAnim.SetTrigger("catchTrigger");
            print("I'm catching");
            iwasThrown = false;
        }
        //iwasThrown = false;
    }
    IEnumerator WaitaSecond()
    {
        //iwasThrown = true;
        yield return new WaitForSeconds(0.5f); //how long to wait till it returns
        iwasThrown = true;
    }



    void Update()
    {
        Moving();
        Throw();
        Catch();

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




        }
    }
}