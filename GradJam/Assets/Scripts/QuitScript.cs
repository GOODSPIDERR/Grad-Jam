using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitScript : MonoBehaviour
{
    //While this is active, sends to the main menu if you press Escape
    void OnEnable()
    {
        StartCoroutine("SelfDisable");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //this is the second "ESC" press. First is triggered in RespawnerScript
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator SelfDisable()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}

