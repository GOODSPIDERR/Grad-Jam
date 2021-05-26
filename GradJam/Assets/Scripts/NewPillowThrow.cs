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
    bool comingBack = false, initialPass = false, forceComeBack = false;
    Animator pillowAnimator;
    CapsuleCollider capsuleCollider;
    SphereCollider sphereCollider;
    AudioSource hitSound;
    public GameObject featherVFX;
    Transform mainCamera;
    float i = 0f;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        pillow = GameObject.FindGameObjectWithTag("Pillow").GetComponent<Transform>();
        pillowAnimator = GameObject.FindGameObjectWithTag("Pillow").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        sphereCollider = GetComponent<SphereCollider>();
        hitSound = GetComponent<AudioSource>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        StartCoroutine(InitialPass());
    }

    void Update()
    {
        if (rb.velocity.magnitude < 1.5f && !comingBack && initialPass)
        {
            capsuleCollider.enabled = false;
            comingBack = true;
        }

        if (!forceComeBack)
        {
            if (Vector3.Distance(transform.position, pillow.position) > 0.275f && comingBack)
            {
                i += 2f * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, pillow.position, i);
            }
            else if (Vector3.Distance(transform.position, pillow.position) <= 0.275f && comingBack)
            {
                pillowAnimator.SetTrigger("catchTrigger");
                Destroy(gameObject);
            }
        }

        else
        {
            i += 2f * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pillow.position, i);

            if (Vector3.Distance(transform.position, pillow.position) <= 0.275f)
            {
                pillowAnimator.SetTrigger("catchTrigger");
                Destroy(gameObject);
            }
        }

    }

    IEnumerator InitialPass()
    {
        yield return new WaitForSeconds(0.5f);
        initialPass = true;
        yield return new WaitForSeconds(1f);
        forceComeBack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Pillow") && !other.CompareTag("FlyingPillow") && !other.CompareTag("Player"))
        {
            sphereCollider.enabled = false;
            hitSound.pitch = Random.Range(0.95f, 1.05f);
            hitSound.Play();
            Instantiate(featherVFX, transform.position, transform.rotation);
            mainCamera.localPosition = new Vector3(0, 0, 0);
            Sequence shakeSequence = DOTween.Sequence();
            shakeSequence.Append(mainCamera.DOShakePosition(0.25f, new Vector3(0.25f, 0.25f, 0f), 30, 10, false, true));
            shakeSequence.Append(mainCamera.DOLocalMove(new Vector3(0, 0, 0), 0.4f));
            shakeSequence.Play();
            StartCoroutine(ShortDelayComeback());
        }

        IEnumerator ShortDelayComeback()
        {
            yield return new WaitForSeconds(0.25f);
            comingBack = true;
        }

    }
}
