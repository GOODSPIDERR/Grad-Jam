using UnityEngine;
using System.Collections;

public class SheepHitter : MonoBehaviour
{

    public static Vector3 sheepLocation;
    //The sheeps' current health point total
    public int currentHealth = 3;
    private AudioSource sheepSound;



    public void Start()
    {
        sheepLocation = gameObject.transform.position;
        sheepSound = GetComponent<AudioSource>();

    }
    public void Damage(int damageAmount)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;
        sheepSound.Play();
        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            sheepSound.Play();
            //if health has fallen below zero, make it disappear 
            gameObject.SetActive(false);
        }
    }



}