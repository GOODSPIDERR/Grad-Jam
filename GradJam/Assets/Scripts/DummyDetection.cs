using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDetection : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pillow" || other.tag == "FlyingPillow")
        {
            animator.SetTrigger("Hit!");
        }
    }
}
