using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Component that handles sending necessary data to the GameManager
 * Should be used by other components in the player gameobject when they want
 * to contact the gameManager
 */
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;
    
    public void UpdatePlayerHealth(float currentHealth, float maxHealth)
    {
        if (gm)
        {
            gm.UpdatePlayerHealthBar(currentHealth, maxHealth);
        }
    }

    public void PlayerDefeat()
    {
        if (gm)
        {
            gm.DefeatScreen();
        }
    }
    
}
