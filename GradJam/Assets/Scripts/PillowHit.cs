using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PillowHit : MonoBehaviour
{
    private Animator pillowAnim;
    //Part of the old boomerang pillow system; don't need this anymore
    //public bool iwasThrown;
    public bool canAttack = true;
    Transform mainCamera;
    public GameObject featherVFX;
    public Transform featherSpawn;
    public AudioSource swipeSound, hitSound;
    public GameObject pillowPrefab;
    public SkinnedMeshRenderer pillowMeshRenderer;
    Transform player;
    bool hasHit;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        pillowAnim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void Moving()
    {
        //Could optimize eventually by moving this function out of update and just check in else statement for key release 
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S)) || Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.A))) //Detects if you're moving and plays the running animation
        {
            pillowAnim.SetBool("movingBool", true);
        }
        else
        {
            pillowAnim.SetBool("movingBool", false); //Stops the animation if you're not holding any of the aforementioned keys
        }
    }
    public void Throw()
    {
        pillowMeshRenderer.enabled = false;
        Instantiate(pillowPrefab, transform.position, mainCamera.rotation);
    }
    public void Catch()
    {
        pillowMeshRenderer.enabled = true;
    }

    //Part of the old boomerang pillow system; don't need this anymore
    /*
    IEnumerator WaitaSecond()
    {
        yield return new WaitForSeconds(0.5f); //how long to wait till it returns.
        // if time is adjusted we need to change in Boomerang script as well. 
        iwasThrown = true;
    }
    */

    void Update()
    {
        if (Input.GetMouseButton(1) && canAttack) //If you click RMB and can attack, throws the pillow
        {
            //This needs to be slightly reworked
            //Why? It's perfect
            pillowAnim.SetTrigger("throwTrigger");
            swipeSound.pitch = Random.Range(0.95f, 1.05f);
            swipeSound.Play();

            //Part of the old boomerang system
            /*
            GameObject clone = Instantiate(pillowPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            pillowMeshRenderer.enabled = false;
            StartCoroutine(WaitaSecond());
            */
        }

        Moving();

        if (Input.GetMouseButton(0) && canAttack) //If you click LMB and can attack, swings the pillow
        {
            hasHit = false;
            pillowAnim.SetTrigger("swingTrigger");
            swipeSound.pitch = Random.Range(0.95f, 1.05f);
            swipeSound.Play();
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
