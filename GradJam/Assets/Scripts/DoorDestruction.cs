using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestruction : MonoBehaviour
{
    public bool doubleDoorBool = false, firstDoor = false;
    public GameObject singleDoor, doubleDoor, countDown;
    public CountdownClock clockTimer;


    private void OnTriggerEnter(Collider other) //When the door detects a pillow, it deletes itself and spawns the animated shattered door
    {
        if (other.tag == "Pillow")
        {
            if (firstDoor) //If true, starts the timer
            {
                countDown.SetActive(true);
                //clockTimer.race = true;
            }

            if (!doubleDoorBool) //If it's a single door, spawn a single door
            {
                Instantiate(singleDoor, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            }
            else //Otherwise, if it's a double door, spawn a double door
            {
                Instantiate(doubleDoor, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
