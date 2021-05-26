using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewPillowThrow : MonoBehaviour
{
    Rigidbody rb;
    public float throwForce = 10f;
    Transform pillow;
    bool comingBack = false;
    bool initialPass = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pillow = GameObject.FindGameObjectWithTag("Pillow").GetComponent<Transform>();
        rb.AddForce(Vector3.forward * throwForce, ForceMode.Impulse);
        StartCoroutine(PillowTiming());
    }

    void Update()
    {
        if (rb.velocity.magnitude < 2f && !comingBack && initialPass)
        {
            transform.DOMove(pillow.position, 0.25f);
            comingBack = true;
        }
    }

    IEnumerator PillowTiming()
    {
        yield return new WaitForSeconds(0.3f);
        initialPass = true;
    }
}
