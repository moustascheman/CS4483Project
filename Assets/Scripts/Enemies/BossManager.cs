using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;

    [SerializeField]
    private string bossName;


    public void Awake()
    {
        updateBossName(bossName);
    }


    public void updateHealthBar(float currentHealth, float maxHealth)
    {
        if (gm)
        {
            gm.UpdateBossHealthBar(currentHealth, maxHealth);
        }
    }

    public void updateBossName(string name)
    {
        if (gm)
        {
            gm.UpdateBossNameText(name);
        }
    }

    public void PlayerVictory()
    {
        gm.VictoryScreen();
    }


}
