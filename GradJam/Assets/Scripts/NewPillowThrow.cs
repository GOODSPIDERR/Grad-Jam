using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewPillowThrow : MonoBehaviour
{
    Rigidbody rb;
    public float throwForce = 10f;
    Transform pillow;
    Transform player;
    bool comingBack = false, initialPass = false;
    Animator pillowAnimator;
    public BoxCollider collision, hitBox;
    AudioSource hitSound;
    public GameObject featherVFX;
    Transform mainCamera;
    bool hasHit = false;
    float i = 0f;
    void Start()
    {
        //Getters
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        pillow = GameObject.FindGameObjectWithTag("Pillow").GetComponent<Transform>();
        pillowAnimator = GameObject.FindGameObjectWithTag("Pillow").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hitSound = GetComponent<AudioSource>();

        //Initial functions
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        StartCoroutine(InitialPass());
        transform.Rotate(new Vector3(75, 0, 0), Space.Self);
    }

    void Update()
    {
        //Behavior of the pillow if it's not coming back
        if (!comingBack)
        {
            //Spin the pillow if it hasn't hit anything yet
            if (!hasHit)
                transform.Rotate(1080f * Time.deltaTime, 0, 0, Space.Self);

            //If the pillow has slowed down enough and it waited its minimum time, disable its collider and start returning it to the player
            if (rb.velocity.magnitude < 1.5f && initialPass)
            {
                collision.enabled = false;
                comingBack = true;
            }
        }

        //Behavior of the pillow if it's coming back
        else
        {
            //The return is handled by a lerp so it takes the same amount of time regardless of how far the pillow is from the player
            i += 2f * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pillow.position, i);

            //If the pillow is close enough to the player, it deletes itself and plays the catch animation
            if (Vector3.Distance(transform.position, pillow.position) <= 0.275f)
            {
                pillowAnimator.SetTrigger("catchTrigger");
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasHit)
        {
            //Makes sure that this only triggers once per swing
            hasHit = true;

            //If removed, one pillow throw can hit multiple targets, which is cool. Only problem is: it can also hit the same target twice
            hitBox.enabled = false;

            //Spawns the feather VFX
            Instantiate(featherVFX, transform.position, transform.rotation);

            //Camera shake
            mainCamera.localPosition = new Vector3(0, 0, 0);
            Sequence shakeSequence = DOTween.Sequence();
            shakeSequence.Append(mainCamera.DOShakePosition(0.25f, new Vector3(0.25f, 0.25f, 0f), 30, 10, false, true));
            shakeSequence.Append(mainCamera.DOLocalMove(new Vector3(0, 0, 0), 0.4f));
            shakeSequence.Play();

            //Sound stuff is handled by the Feather VFX prefab now
            //hitSound.pitch = Random.Range(0.95f, 1.05f);
            //hitSound.Play();

            //Starts a short coroutine with delayed comeback
            StartCoroutine(ShortDelayComeback());
        }

    }
    IEnumerator InitialPass() //Executed at the pillow's spawn for timing some bools
    {
        //Small delay after it's thrown so it doesn't return to the player immediately
        yield return new WaitForSeconds(0.5f);
        initialPass = true;

        //If the pillow doesn't hit anything after this delay, force the return to the player and disable the collider
        yield return new WaitForSeconds(1f);
        comingBack = true;
        collision.enabled = false;
    }

    IEnumerator ShortDelayComeback() //Short coroutine which forced a comeback (called after it hit something)
    {
        yield return new WaitForSeconds(0.35f);
        comingBack = true;
        collision.enabled = false;
    }


}
