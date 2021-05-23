using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHitter : MonoBehaviour
{

    public static Vector3 sheepLocation;
    //The sheeps' current health point total
    public int currentHealth;
    //private AudioSource sheepSound;
    //public List<AudioClip> mySheepSounds = new List<AudioClip>();
    public bool bigSheep;
    public bool smolSheep;



    public void Start()
    {
        sheepLocation = gameObject.transform.position;
        if (bigSheep) currentHealth = 3;
        if (smolSheep) currentHealth = 1;
        //sheepSound = GetComponent<AudioSource>();
        //AudioSource[] sounds;
        //sounds = GetComponents<AudioSource>();
        //sheepSound = sounds[0];

    }
    public void Damage(int damageAmount)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;
        //sheepSound.PlayOneShot(mySheepSounds[Random.Range(0, 3)]);
        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            //sheepSound.PlayOneShot(mySheepSounds[Random.Range(0, 3)]);
            //if health has fallen below zero, make it disappear 
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log(collision.gameObject.name);
        if (other.gameObject.CompareTag("Pillow"))
        {
            Damage(1);
            Debug.Log("I'm hit noooo");
        }
        Debug.Log("hit");
    }

}