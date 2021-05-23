using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHitter : MonoBehaviour
{

    public static Vector3 sheepLocation;
    //The sheeps' current health point total
    public int currentHealth;
    public GameObject poofPrefab;
    private AudioSource sheepSound;
    public List<AudioClip> mySheepSounds = new List<AudioClip>();
    public bool bigSheep;
    public bool smolSheep;




    public void Start()
    {
        sheepLocation = gameObject.transform.position;
        if (bigSheep) currentHealth = 3;
        if (smolSheep) currentHealth = 1;
        sheepSound = GetComponent<AudioSource>();


    }
    public void Damage(int damageAmount)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;
        sheepSound.PlayOneShot(mySheepSounds[Random.Range(0, 2)]);
        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            sheepSound.PlayOneShot(mySheepSounds[Random.Range(0, 2)]);
            //if health has fallen below zero, make it disappear 
            gameObject.SetActive(false);
            GameObject clone;
            clone = Instantiate(poofPrefab, new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z), transform.rotation) as GameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.CompareTag("Pillow"))
        {
            Damage(1);
            //Debug.Log("I'm hit noooo");
        }
        //Debug.Log("hit");
    }
}







