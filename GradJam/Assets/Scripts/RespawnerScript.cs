using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RespawnerScript : MonoBehaviour
{
    public GameObject alarmClock;
    public GameObject quitObject;

    void Update()
    {
        
        if (Input.GetKeyDown("escape"))
        {
            quitObject.SetActive(true); //displays quit confirmation 
        }

        if (transform.position.y < -10f) //if you fall off
        {
            transform.position = new Vector3(-70.6f, 22, 267.8f); //spawn back before the jumping platforms
        }

    }
    /*
    double distance()
    {
        double x, y, z, dist;
        x = transform.position.x - alarmClock.transform.position.x;
        y = transform.position.y - alarmClock.transform.position.y;
        z = transform.position.z - alarmClock.transform.position.z;
        x = x * x;
        y = y * y;
        z = z * z;
        dist = Math.Sqrt(x + y + z);
        return dist;
    }
    */
}
