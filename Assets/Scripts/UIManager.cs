using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField]
    private GameObject pauseScreen;

    public void OnPause()
    {
        TogglePause();
    }


    public void TogglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            isPaused = false;
        }
        pauseScreen.SetActive(isPaused);
    }

    public void returnToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void exitGame()
    {
        Time.timeScale = 1.0f;
        Application.Quit();
    }

}
