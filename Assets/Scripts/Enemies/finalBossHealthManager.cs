using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBossHealthManager : MonoBehaviour, IHealthManager
{

    [SerializeField]
    private float MaxHealth = 20f;
    [SerializeField]
    private float currentHealth = 20f;

    [SerializeField]
    private EnemyController controller;

    private int currentComboStage = 0;

    private bool isInvulnerable = false;

    private Coroutine invulnRoutine;

    [SerializeField]
    private float invulnTimer;

    [SerializeField]
    private SpriteRenderer sRenderer;

    private bool isBlinking = false;


    [SerializeField]
    private float bufferTime;

    private Coroutine bufferRoutine;

    private bool isInBufferTime = false;

    private Coroutine blinkRoutine;


    [SerializeField]
    private BossManager bm;

    [SerializeField]
    private ActorSoundManager sm;

    public void Kill()
    {
        controller.DeathLock();
        controller.pauseBehavior();
        controller.playAnim("die");
        StartCoroutine(PlayerVictorious());
        Destroy(gameObject, 4f);
    }

    public void KillQuietly()
    {

    }


    public void DealDamage(float dam, float hitstun, int comboStage)
    {
        if(comboStage > currentComboStage)
        {
            currentHealth -= dam;
            currentComboStage = comboStage;
            bm.updateHealthBar(currentHealth, MaxHealth);
            sm.PlayEffect("dam");
            if (currentHealth <= 0)
            {
                Kill();
            }
            else
            {
                DamageEffects();
            }

        }
    }


    private void DamageEffects()
    {
        controller.pauseBehavior();
        controller.playAnimWithCancels("dam1");
        if (isInvulnerable)
        {
            StopCoroutine(invulnRoutine);
        }

        isInvulnerable = true;
        invulnRoutine = StartCoroutine(iframes());

        if (isBlinking)
        {
            StopCoroutine(blinkRoutine);
        }
        isBlinking = true;
        blinkRoutine = StartCoroutine(blink());
    }


    private void EndStun()
    {
        controller.playAnim("idle");
        if (isInBufferTime)
        {
            StopCoroutine(bufferRoutine);
        }
        controller.pauseBehavior();
        isInBufferTime = true;
        bufferRoutine = StartCoroutine(buffering());
    }



    private IEnumerator buffering()
    {
        yield return new WaitForSeconds(bufferTime);
        isInBufferTime = false;
        controller.resumeBehavior();
    }



    private IEnumerator iframes()
    {
        yield return new WaitForSeconds(invulnTimer);
        currentComboStage = 0;
        isInvulnerable = false;
    }


    IEnumerator blink()
    {
        isBlinking = true;
        bool flashing = false;
        while (currentComboStage > 0)
        {
            if (true)
            {
                if (!flashing)
                {
                    sRenderer.enabled = true;
                    flashing = true;
                }
                else
                {
                    sRenderer.enabled = false;
                    flashing = false;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
        sRenderer.enabled = true;
        isBlinking = false;
    }

    IEnumerator PlayerVictorious()
    {
        yield return new WaitForSeconds(3f);
        bm.PlayerVictory();
    }

}
