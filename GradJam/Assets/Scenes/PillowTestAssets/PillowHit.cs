using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PillowHit : MonoBehaviour
{
    private Animator pillowAnim;
    public bool leftHit = true;
    public bool iwasThrown;
    public BoxCollider reach;
    private bool danger = false;
    private SheepHitter sheep;
    private Collider hittingStuff;
    public bool canAttack = true;
    public Transform mainCamera;
    public GameObject featherVFX;
    public Transform featherSpawn;
    public AudioSource swipeSound, hitSound;
    bool hasHit;

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
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S)) || Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.A)))
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
        if (Input.GetMouseButtonDown(1) && canAttack) //i don't know why the fuck this is not working
        {
            pillowAnim.SetTrigger("throwTrigger");
            //print("I'm throwing");
            //iwasThrown = true;
            StartCoroutine(WaitaSecond());
        }
    }
    public void Catch()
    {
        if (iwasThrown)
        {
            pillowAnim.SetTrigger("catchTrigger");
            //print("I'm catching");
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

        if (Input.GetMouseButton(0) && canAttack)
        {
            hasHit = false;
            pillowAnim.SetTrigger("swingTrigger");
            swipeSound.pitch = Random.Range(0.95f, 1.05f);
            swipeSound.Play();

            /*
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
            */
        }
    }


    private void OnTriggerEnter(Collider other) //Screenshake whenever you hit anything
    {
        if (!hasHit)
        {
            mainCamera.localPosition = new Vector3(0, 0, 0);
            hasHit = true;
            Sequence shakeSequence = DOTween.Sequence();
            shakeSequence.Append(mainCamera.DOShakePosition(0.6f, new Vector3(0.4f, 0.4f, 0f), 30, 10, false, true));
            shakeSequence.Append(mainCamera.DOLocalMove(new Vector3(0, 0, 0), 0.4f));
            shakeSequence.Play();
            Instantiate(featherVFX, featherSpawn.position, transform.rotation);
            hitSound.pitch = Random.Range(0.95f, 1.05f);
            hitSound.Play();
        }

    }
}
