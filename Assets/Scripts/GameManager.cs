using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        uiMan.UpdateBossName(name);
    }






}
