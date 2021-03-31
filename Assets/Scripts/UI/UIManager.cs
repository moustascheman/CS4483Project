using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //This should be a component of the gameObject that contains the screenspace canvas for the game
    private bool isPaused = false;

    [SerializeField]
    private GameObject pauseScreen;


    [SerializeField]
    private Slider playerHealthBar;

    [SerializeField]
    private Slider bossHealthBar;

    [SerializeField]
    private TextMeshProUGUI areaMessageBox;

    [SerializeField]
    private TextMeshProUGUI bossHealthBarName;

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


    public void UpdatePlayerHealthSlider(float currentHealth, float maxHealth)
    {
        playerHealthBar.maxValue = maxHealth;
        playerHealthBar.value = currentHealth;
    }

    public void UpdateBossHealthSlider(float currentHealth, float maxHealth)
    {
        bossHealthBar.maxValue = maxHealth;
        bossHealthBar.value = currentHealth;
    }

    public void UpdateBossName(string name)
    {
        bossHealthBarName.text = name;
    }
}
