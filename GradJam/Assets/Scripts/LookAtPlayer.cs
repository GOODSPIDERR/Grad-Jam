using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //Find the player
    }

    void Update()
    {
        transform.LookAt(player); //Tutorial text that looks at the player
    }
}
