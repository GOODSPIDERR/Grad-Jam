using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestruction : MonoBehaviour
{
    public bool doubleDoorBool = false, firstDoor = false;
    public GameObject singleDoor, doubleDoor;
    public CountdownClock clockTimer;

    public GameObject countDown;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pillow")
        {
            if (firstDoor)
            {
                countDown.SetActive(true);
                clockTimer.race = true;
            }

            if (!doubleDoorBool)
            {
                Instantiate(singleDoor, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            }
            else
            {
                Instantiate(doubleDoor, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
