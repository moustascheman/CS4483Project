using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Behavior : EnemyBehavior
{

    private int lastProjectilePos = 0;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject Fireball;

    [SerializeField]
    private DamageSource dm;

    [SerializeField]
    private List<Transform> positions;


    [SerializeField]
    private Transform highFirePoint;

    [SerializeField]
    private Transform lowFirePoint;


    [SerializeField]
    private float combo1Damage = 2;

    [SerializeField]
    private float combo2Damage = 3;

    [SerializeField]
    private int fireBallCount = 0;

    [SerializeField]
    private float projectileVel;

    [SerializeField]
    private float minMeleeDistance;

    [SerializeField]
    private int fireballsPerSequence = 3;


    void FixedUpdate()
    {
        if (aggro && !busy && !coolingDown)
        {
            if (fireBallCount > 0 && fireBallCount < fireballsPerSequence)
            {
                //resume firing fireballs
                Fire();

            }
            else
            {
                //get horizontal distance to target
                Vector2 diff = transform.position - target.transform.position;
                float distance = Mathf.Abs(diff.x);
                int chance = Random.Range(0, 99);
                if (distance <= minMeleeDistance)
                {
                    //higher chance of melee combo
                    if (chance < 70)
                    {
                        //melee
                        controller.FaceTowards(target.transform);
                        MeleeAttack();

                    }
                    else if (chance < 80)
                    {
                        //fire
                        controller.FaceTowards(target.transform);
                        Fire();

                    }
                    else if (chance < 100)
                    {
                        //teleport to new position
                        Teleport();

                    }

                }
                else
                {
                    if (chance < 80)
                    {
                        //fire
                        controller.FaceTowards(target.transform);
                        Fire();
                    }
                    else if (chance < 100)
                    {
                        //teleport
                        Teleport();
                    }

                }

            }


        }
    }

    private void Fire()
    {
        print("START FIRING");
        busy = true;
        if(fireBallCount == 0)
        {
            //randomly determine the first fireball position;
            int initPos = Random.Range(0, 1);
            lastProjectilePos = initPos;
        }
        fireBallCount++;
        controller.playAnim("fire");
    }


    private void FireProjectile()
    {
        if(lastProjectilePos == 0)
        {
            //Fire High
            lastProjectilePos = 1;
            GameObject fireball = Instantiate(Fireball, highFirePoint);
            fireball.transform.localScale = (new Vector3(-1, 1, 1));
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            rb.velocity = controller.forwardDir * projectileVel;
            Destroy(fireball, 5f);
        }
        else
        {
            //fire Low
            lastProjectilePos = 0;
            GameObject fireball = Instantiate(Fireball, lowFirePoint);
            fireball.transform.localScale = (new Vector3(-1, 1, 1));
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            rb.velocity = controller.forwardDir * projectileVel;
            Destroy(fireball, 5f);
        }
    }

    private void MeleeAttack()
    {
        busy = true;
        controller.playAnim("attack");
    }


    private void Teleport()
    {
        busy = true;
        controller.playAnim("tp");
    }

    private void endTP()
    {
        StartCoroutine(coolDownMinor(0.1f));
        busy = false;
    }


    private void EndFireballAnim()
    {
        
        if(fireBallCount >= fireballsPerSequence)
        {
            fireBallCount = 0;
            StartCoroutine(startCoolDown());
        }
        busy = false;
    }

    private void incrementComboAttack() 
    {
        dm.comboStage = 2;
        dm.currentDamage = combo2Damage;
    }


    private void EndMeleeAnimation()
    {
        //reset combo data
        dm.currentDamage = combo1Damage;
        dm.comboStage = 1;
        StartCoroutine(startCoolDown());
        busy = false;
    }


    private void TeleportToLoc()
    {
        int ind = Random.Range(0, positions.Count - 1);
        rb.MovePosition(positions[ind].position);
        controller.FaceTowards(target.transform);
    }

    private IEnumerator startCoolDown()
    {
        coolingDown = true;
        controller.playAnim("idle");
        yield return new WaitForSeconds(coolDownTime);
        coolingDown = false;
    }

    private IEnumerator coolDownMinor(float time)
    {
        coolingDown = true;
        controller.playAnim("idle");
        yield return new WaitForSeconds(time);
        coolingDown = false;
    }



}
