using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHitter : MonoBehaviour
{

    public static Vector3 sheepLocation;
    //The sheeps' current health point total
    public int currentHealth = 3;
    private AudioSource sheepSound;
    public List<AudioClip> mySheepSounds = new List<AudioClip>();
    public bool bigSheep;



    public void Start()
    {
        sheepLocation = gameObject.transform.position;
        sheepSound = GetComponent<AudioSource>();

    }
    public void Damage(int damageAmount)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;
        sheepSound.PlayOneShot(mySheepSounds[Random.Range(0, 3)]);
        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            sheepSound.PlayOneShot(mySheepSounds[Random.Range(0, 3)]);
            //if health has fallen below zero, make it disappear 
            gameObject.SetActive(false);
        }
    }



}