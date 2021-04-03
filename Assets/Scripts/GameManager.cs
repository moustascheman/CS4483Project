using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uiMan;
    

    public void PrintToAreaTextBox(string message)
    {

    }

    public void UpdatePlayerHealthBar(float currentHealth, float maxHealth)
    {
        uiMan.UpdatePlayerHealthSlider(currentHealth, maxHealth);
    }

    public void UpdateBossHealthBar(float currentHealth, float maxHealth)
    {
        uiMan.UpdateBossHealthSlider(currentHealth, maxHealth);
    }

    public void UpdateBossNameText(string name)
    {
        uiMan.ActivateBossUI();
        uiMan.UpdateBossName(name);
    }


    public void LoadLevelSelect()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void reloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VictoryScreen()
    {
        Time.timeScale = 0f;
        uiMan.ActivateVictoryScreen();
    }

    public void DefeatScreen()
    {
        Time.timeScale = 0f;
        uiMan.ActivateDefeatScreen();
    }





}
