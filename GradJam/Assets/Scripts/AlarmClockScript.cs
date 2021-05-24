using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlarmClockScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //If the alarm clock detects the pillow, it starts the transition
    {
        if (other.tag == "Pillow")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


}
