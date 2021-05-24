using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEndScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("EndgameTransition");
    }

    void Update()
    {
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
