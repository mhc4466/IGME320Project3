using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    // Simple method loads main gameplay scene when called
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    // Simple method will quit the entire application when called
    public void QuitGame()
    {
        // Debug.Log("Quit");
        Application.Quit();
    }
}

