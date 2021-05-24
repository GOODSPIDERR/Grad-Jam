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
    public GameObject pillowPrefab;
    public Camera cam;
    public SkinnedMeshRenderer pillowMeshRenderer;
    bool hasHit;

    void Start()
    {
        pillowAnim = gameObject.GetComponent<Animator>();
    }

    //Part of the old swinging mechanic; not needed anymore
    /*
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
    */
    public void Moving()
    {
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S)) || Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.A))) //Detects if you're moving and plays the running animation
        {
            pillowAnim.SetBool("movingBool", true);
            //pillowAnim.SetTrigger("movementTrigger");
            //print("I'm moving");
        }
        else
        {
            pillowAnim.SetBool("movingBool", false); //Stops the animation if you're not holding any of the aforementioned keys
        }
    }
    public void Throw()
    {
        if (Input.GetMouseButtonDown(1) && canAttack) //If you click RMB and can attack, throws the pillow
        {
            //This needs to be slightly reworked
            pillowAnim.SetTrigger("throwTrigger");
            GameObject clone = Instantiate(pillowPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            pillowMeshRenderer.enabled = false;
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


        //Copied this from PillowRaycast
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if (Input.GetMouseButton(0) && canAttack) //Melee attack
        {
            hasHit = false;
            pillowAnim.SetTrigger("swingTrigger");
            swipeSound.pitch = Random.Range(0.95f, 1.05f);
            swipeSound.Play();

            //Part of the old swinging mechanic; not needed anymore
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
        if (!hasHit) //Makes sure that this only triggers once per swing
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

        if (other.GetComponent<Rigidbody>() != null) //If the thing you hit happens to have a rigidbody, applies a force in the direction you're facing
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Vector3 direction = (transform.position - other.transform.position).normalized;
            rb.AddForce(-direction, ForceMode.Impulse);
        }

    }
}
