using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() //Loads the next scene in the index
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() //Closes the game
    {
        Application.Quit();
    }

    private void Update() //Tilts the UI according to the mouse position
    {
        Vector2 mouseOffset = new Vector2(Screen.width / 2 - Input.mousePosition.x, Screen.height / 2 - Input.mousePosition.y);

        transform.localRotation = Quaternion.Euler(-mouseOffset.y * 0.01f, mouseOffset.x * 0.01f, 0);
    }
}
