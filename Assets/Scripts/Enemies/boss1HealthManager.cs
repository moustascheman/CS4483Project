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

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Material defaultMat;

    [SerializeField]
    private Material flashMat;

    [SerializeField]
    private BossManager bm;

    private bool isFlashing = false;
    private Coroutine flashRoutine;

    public void Kill()
    {
        controller.playAnim("die");
        controller.pauseBehavior();
        
        Destroy(gameObject, 4f);
        StartCoroutine(PlayerVictorious());
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
                if (bm)
                {
                    bm.updateHealthBar(currentHealth, MaxHealth);
                }
                Kill();
            }
            else
            {
                currentHealth -= dam;
                if (bm)
                {
                    bm.updateHealthBar(currentHealth, MaxHealth);
                }
                sr.material = flashMat;
                if (isFlashing)
                {
                    StopCoroutine(flashRoutine);
                }

                isFlashing = true;
                flashRoutine = StartCoroutine(Flash(0.2f));
            }
        }
    }


    private void resetFlash()
    {
        sr.material = defaultMat;
    }

    private IEnumerator Flash(float time)
    {
        yield return new WaitForSeconds(time);
        sr.material = defaultMat;
    }



    IEnumerator PlayerVictorious()
    {
        yield return new WaitForSeconds(3f);
        bm.PlayerVictory();
    }

}
