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
    private float invulnTimer;


    [SerializeField]
    private string deathAnim = "wizDie";

    [SerializeField]
    private string damAnim = "wizDam";


    private float currentComboStage = 0;

    [SerializeField]
    private bool isInHitStun = false;

    private Coroutine hitstunRoutine;

    [SerializeField]
    private List<GameObject> hBoxes;

    [SerializeField]
    private List<GameObject> Drops;

    [SerializeField]
    private float dropPercent = 10;

    [SerializeField]
    private float dropInitialJump = 0.5f;



   public void Kill()
    {
        //AUDIO sfx CODE
        SoundManagerScript.PlaySound("enemyDieSound");

        foreach (GameObject obj  in hBoxes)
        {
            obj.SetActive(false);
            
        }
        StopCoroutine(hitstunRoutine);

        controller.pauseBehavior();
        controller.playAnim(deathAnim);
        Drop();
        Destroy(gameObject, 4f);
        
    }

    private void Drop()
    {
        float chance = Random.Range(1, 100);
        if(chance <= dropPercent)
        {
            //Drop Pickup
            int index = Random.Range(0, Drops.Count - 1);
            GameObject dropObj = Drops[index];
            GameObject fDrop = Instantiate(dropObj, gameObject.transform);
            Rigidbody2D rb = fDrop.GetComponent<Rigidbody2D>();
            rb.velocity = dropInitialJump*Vector2.up;
            fDrop.transform.parent = null;
            Destroy(fDrop, 10f);
        }
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
                currentHealth -= dam;
                Kill();
                //AUDIO sfx CODE
                SoundManagerScript.PlaySound("enemyHitReactSound");

            }
            else
            {
                controller.playAnimWithCancels(damAnim);
                if (isInHitStun)
                {
                    StopCoroutine(hitstunRoutine);
                    currentHealth -= dam;
                    currentComboStage = comboStage;
                    hitstunRoutine = StartCoroutine(HitStun());
                    //AUDIO sfx CODE
                    SoundManagerScript.PlaySound("hitReactSound");
                }
                else
                {
                    currentHealth -= dam;
                    currentComboStage = comboStage;
                    hitstunRoutine = StartCoroutine(HitStun());
                    //AUDIO sfx CODE
                    SoundManagerScript.PlaySound("hitReactSound");
                }
            }
        }
    }
      
    public IEnumerator HitStun()
    {
        controller.pauseBehavior();
        isInHitStun = true;
        yield return new WaitUntil(() => !isInHitStun);
        currentComboStage = 0;
        controller.resumeBehavior();
    }

    private void EndStun()
    {
        isInHitStun = false;
    }

}
