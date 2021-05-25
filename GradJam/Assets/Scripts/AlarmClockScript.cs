using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlarmClockScript : MonoBehaviour
{

    [Range(0.0f, 1.0f)]
    public float setTimeScale = 1f;
    public GameObject destructionClock;
    public GameObject sceneFade;
    public CountdownClock clock;
    private void OnTriggerEnter(Collider other) //If the alarm clock detects the pillow, it starts the transition
    {
        if (other.tag == "Pillow")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            clock.race = false;
            sceneFade.SetActive(true);
            Time.timeScale = setTimeScale;
            Instantiate(destructionClock, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
