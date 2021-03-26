using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1HealthManager : MonoBehaviour, IHealthManager
{
    [SerializeField]
    private float MaxHealth = 20f;
    [SerializeField]
    private float currentHealth = 20f;

    [SerializeField]
    private EnemyController controller;

    private float currentComboStage = 0;
    public void Kill()
    {
        controller.playAnim("die");
        Destroy(gameObject, 4f);
    }


    public void KillQuietly()
    {
        Destroy(gameObject);
    }


    public void DealDamage(float dam, float hitstun, int comboStage)
    {
        if (comboStage > currentComboStage)
        {
            //attack hits
            if ((currentHealth - dam) <= 0)
            {
                currentHealth -= dam;
                Kill();
            }
            else
            {
                currentHealth -= dam;
            }
        }
    }
}
