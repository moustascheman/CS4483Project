using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth :  MonoBehaviour, IHealthManager
{

    [SerializeField]
    private float MaxHealth;
    [SerializeField]
    private float currentHealth;

    [SerializeField]
    private float invulnTime;

    [SerializeField]
    private bool isStunned = false;
    private int currentComboStage = 0;

    [SerializeField]
    private float knockbackDist;

    [SerializeField]
    private CombatManager cmb;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private List<GameObject> hboxList;

    [SerializeField]
    private SpriteRenderer sRenderer;




    private Coroutine sTimer;
    private Coroutine iframes;
    private Coroutine blinkRoutine;

    private bool isBlinking = false;
    public void Kill()
    {
        Debug.Log("KILL PLAYER");
        cmb.stunPlayer();
        anim.Play(PlayerAnimStates.DEATH_ANIM);
        foreach(GameObject obj in hboxList)
        {
            obj.SetActive(false);
        }
    }


    public void KillQuietly()
    {

    }

    private void Heal(float amount)
    {
        if((currentHealth + amount) > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        else
        {
            currentHealth += amount;
        }
    }


    public void DealDamage(float dam, float hitstun, int comboStage)
    {
        if(dam < 0)
        {
            Heal(-1 * dam);
        }
        else if(comboStage > currentComboStage)
        {
            currentHealth -= dam;
            currentComboStage = comboStage;
            if(currentHealth <= 0)
            {
                Kill();
            }
            else
            {
                damageEffects();
            }
        }
    }

    private void damageEffects()
    {
        if(iframes != null)
        {
            StopCoroutine(iframes);
           
        }
        isStunned = true;
        cmb.stunPlayer();
        iframes = StartCoroutine(InvulnTimer());
        if(!isBlinking)
        {
            blinkRoutine = StartCoroutine(blink());
            //AUDIO sfx CODE
            SoundManagerScript.PlaySound("hitReactSound");
        }
        
    }


    public void finishStun()
    {
        isStunned = false;
        cmb.endStun();
    }

    IEnumerator InvulnTimer()
    {
        yield return new WaitForSeconds(invulnTime);
        currentComboStage = 0;
    }

    IEnumerator blink()
    {
        isBlinking = true;
        bool flashing = false;
        while(currentComboStage > 0)
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
}
