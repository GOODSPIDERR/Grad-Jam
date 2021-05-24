using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDetection : MonoBehaviour
{
    Animator animator;
    AudioSource hitSound;
    void Start()
    {
        animator = GetComponent<Animator>();
        hitSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pillow" || other.tag == "FlyingPillow") //If the mannequin is hit by a pillow or a flying pillow, do the animation and play the sound <3
        {
            animator.SetTrigger("Hit!");
            hitSound.Play();

        }
    }
}
