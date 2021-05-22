using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        //Debug.Log("Mouse Pos: " + Input.mousePosition);
        //Debug.Log("Height: " + Screen.height + ", Width: " + Screen.width);

        Vector2 mouseOffset = new Vector2(Screen.width / 2 - Input.mousePosition.x, Screen.height / 2 - Input.mousePosition.y);
        //Debug.Log("Mouse Offset: " + mouseOffset);

        transform.localRotation = Quaternion.Euler(-mouseOffset.y * 0.01f, mouseOffset.x * 0.01f, 0);
    }
}
