using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHitter : MonoBehaviour
{
    //This is the script where the sheep prefabs take damage, instantiate poof of smoke vfx and play sheep sound
    //public static Vector3 sheepLocation;
    private int currentHealth;
    public GameObject poofPrefab;
    private AudioSource sheepSound;
    public List<AudioClip> mySheepSounds = new List<AudioClip>();
    public bool bigSheep;
    public bool smolSheep;

    public void Start()
    {
        //sheepLocation = gameObject.transform.position;
        if (bigSheep) currentHealth = 3;
        if (smolSheep) currentHealth = 1;
        sheepSound = GetComponent<AudioSource>();
    }
    public void Damage(int damageAmount)
    {
        //subtract damage amount when Damage function is called. It is called here and also in Boomerangscript

        currentHealth -= damageAmount; //health decrease with each hit. 
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false); //if health falls below zero, make it disappear
            GameObject clone;
            clone = Instantiate(poofPrefab, new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z), transform.rotation) as GameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pillow" || other.tag == "FlyingPillow") //If hit by either pillow or flying pillow, take damage. 
        {
            sheepSound.PlayOneShot(mySheepSounds[Random.Range(0, 2)]);
            Damage(1);
         }
    }
}







