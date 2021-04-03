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
    private string idleAnim = "wizIdle";


    [SerializeField]
    private string deathAnim = "wizDie";

    [SerializeField]
    private string damAnim = "wizDam";


    private float currentComboStage = 0;

    [SerializeField]
    private bool isInHitStun = false;

    private Coroutine hitstunRoutine;
    private Coroutine bufferRoutine;

    private bool isInBufferTime = false;
    [SerializeField]
    private float bufferTime = 0.3f;

    [SerializeField]
    private List<GameObject> hBoxes;

    [SerializeField]
    private List<GameObject> Drops;

    [SerializeField]
    private float dropPercent = 10;

    [SerializeField]
    private float dropInitialJump = 0.5f;

    [SerializeField]
    private ActorSoundManager sm;



   public void Kill()
    {
        controller.pauseBehavior();
        controller.DeathLock();
        //AUDIO sfx CODE
        sm.PlayEffect("die");

        foreach (GameObject obj  in hBoxes)
        {
            obj.SetActive(false);
            
        }
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
            sm.PlayEffect("dam");
            currentHealth -= dam;
            //attack hits
            if((currentHealth) <= 0)
            {
                Kill();
            }
            else
            {
                controller.pauseBehavior();
                controller.playAnimWithCancels(damAnim);
            }
        }
    }
      

    private void EndStun()
    {
        controller.playAnim(idleAnim);
        controller.pauseBehavior();
        if (isInBufferTime)
        {
            StopCoroutine(bufferRoutine);
        }
        isInBufferTime = true;
        bufferRoutine = StartCoroutine(Buffering());

    }

    IEnumerator Buffering()
    {
        yield return new WaitForSeconds(bufferTime);
        isInBufferTime = false;
        controller.resumeBehavior();        
    }

}
