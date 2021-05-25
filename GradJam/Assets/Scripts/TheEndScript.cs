using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEndScript : MonoBehaviour
{
    //Sends you back to the main menu after a given time, letting you hear the entire ending cutscene
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine("EndgameTransition");
        AudioListener.volume = 1f;
    }

    void Update()
    {
        //Used to send you back to the main menu if you pressed Escape. It does so automatically now
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        */
    }

    IEnumerator EndgameTransition()
    {
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene(0);
    }
}
