using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IHealthManager
{
    [SerializeField]
    private float MaxHealth = 20f;
    [SerializeField]
    private float currentHealth = 20f;

    [SerializeField]
    private EnemyController controller;

    [SerializeField]
    //DO NOT CHANGE FROM DEFAULT ZERO ON ANY ENEMY
    private float currentComboStage = 0;

    [SerializeField]
    private bool isInHitStun = false;

    private Coroutine hitstunRoutine;



   public void Kill()
    {
        Debug.Log("KILLING Character");
        StopCoroutine(hitstunRoutine);
        controller.pauseBehavior();
        controller.playAnim("wizDie");
        StartCoroutine(waiter(4f));
    }


    public void KillQuietly()
    {
        Destroy(gameObject);
    }

    public void DealDamage(float dam, float hitstun, int comboStage)
    {
        //attack only connects if combo stage is greater than current stage
        if(comboStage > currentComboStage)
        {
            //attack hits
            if((currentHealth - dam) <= 0)
            {
                Kill();
            }
            else
            {
                controller.playAnimWithCancels("wizDam");
                if (isInHitStun)
                {
                    StopCoroutine(hitstunRoutine);
                    currentHealth -= dam;
                    currentComboStage = comboStage;
                    hitstunRoutine = StartCoroutine(HitStun(hitstun));
                }
                else
                {
                    currentHealth -= dam;
                    currentComboStage = comboStage;
                    hitstunRoutine = StartCoroutine(HitStun(hitstun));
                    
                }
            }
        }
    }
     
    //TODO  Hitstun and IFrames are currently one and the same. They should probably be seperated so that hitstun only determines how long a character is stunned (can't move/perform actions) 
    public IEnumerator HitStun(float time)
    {
        controller.pauseBehavior();
        isInHitStun = true;
        yield return new WaitForSeconds(time);
        currentComboStage = 0;
        isInHitStun = false;
        controller.resumeBehavior();
        Debug.Log(time);
    }

    private IEnumerator waiter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
