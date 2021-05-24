using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitScript : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine("SelfDisable");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

