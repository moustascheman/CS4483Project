using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossBehavior : EnemyBehavior
{
    /*
     * Combine DumbPatrolBehavior with Shooter behavior such that the navigates between points and then performs actions at each point
     */



    [SerializeField]
    private List<Transform> navPoints;

    private int currentIndex;

    private Transform currentPatrolPoint;
    [SerializeField]
    private float minStoppingDistance = 0.5f;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float stunTime;

    [SerializeField]
    private DamageSource dm;

    [SerializeField]
    private int AttackChance;

    [SerializeField]
    private int skipChance;


    [SerializeField]
    private int combo2Chance;

    [SerializeField]
    private int combo3Chance;

    [SerializeField]
    private float combo1Dam = 1;

    [SerializeField]
    private float combo2Dam = 2;

    [SerializeField]
    private float combo3Dam = 3;

    [SerializeField]
    private ActorSoundManager sm;



    private int currentAttack = 0;

    void Awake()
    {
        currentPatrolPoint = navPoints[0];
        currentIndex = 0;
        controller.FaceTowards(currentPatrolPoint);
    }


    void FixedUpdate()
    {
        if (!busy && !coolingDown)
        {
            MoveToTarget();
        }
        else if (coolingDown && !busy)
        {
            controller.playAnim("idle");
        }
    }

    private void MoveToTarget()
    {
        float diff = 0;
        if (transform.position.x >= currentPatrolPoint.position.x)
        {
            diff = transform.position.x - currentPatrolPoint.position.x;
        }
        else if (transform.position.x <= currentPatrolPoint.position.x)
        {
            diff = currentPatrolPoint.position.x - transform.position.x;
        }


        if (diff > minStoppingDistance)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(currentPatrolPoint.position.x, transform.position.y), moveSpeed));
            controller.playAnim("run");
        }
        else
        {
            //Reached patrol point
            PerformAction();
        }
    }

    /*
     * Decide on an action
     * Choose between Skip, Idle, and Attack
     */
    private void PerformAction()
    {
        int chance = Random.Range(0, 99);
        if(chance < skipChance)
        {
            nextTarget();
            controller.FaceTowards(currentPatrolPoint);
        }
        else if(chance < AttackChance)
        {
            busy = true;
            currentAttack = 1;
            controller.FaceTowards(target.transform);
            updateDamageValue();
            controller.playAnim("attack1");
            sm.PlayEffect("attack1");
        }
        else //idle
        {
            StartCoroutine(CoolDown());
        }
    }

    private void nextTarget()
    {
        int numPoints = navPoints.Count;
        int randPoint = Random.Range(0, numPoints - 1);
        if(randPoint == currentIndex)
        {
            currentIndex = randPoint + 1;
        }
        else
        {
            currentIndex = randPoint;
        }
        if (currentIndex >= navPoints.Count)
        {
            currentIndex = 0;
        }
        currentPatrolPoint = navPoints[currentIndex];
    }

    IEnumerator CoolDown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(coolDownTime);
        nextTarget();
        controller.FaceTowards(currentPatrolPoint);
        coolingDown = false;

    }

    private void DetermineNextAttack()
    {
        print("A");
        int chance = Random.Range(0, 99);
        int requiredChance = 0;
        if(currentAttack == 1)
        {
            requiredChance = combo2Chance;
            if (chance < requiredChance)
            {
                currentAttack = 2;
                updateDamageValue();
                controller.playAnim("attack2");
                sm.PlayEffect("attack1");
                return;
            }
        }
        else if(currentAttack == 2)
        {
            print("A3");
            requiredChance = combo3Chance;
            if(chance < requiredChance)
            {
                currentAttack=3;
                updateDamageValue();
                controller.playAnim("attack3");
                sm.PlayEffect("attack1");
                return;
            }
        }
        EndAttack();
    }

    private void EndAttack()
    {
        currentAttack = 0;
        StartCoroutine(CoolDown());
        busy = false;

    }


    private void Interrupt()
    {
        busy = false;
        currentAttack = 0;
    }

    private void updateDamageValue()
    {
        dm.currentHitstun = 0;
        if (currentAttack == 1)
        {
            dm.currentDamage = combo1Dam;
            dm.comboStage = 1;
        }
        else if(currentAttack == 2)
        {
            dm.currentDamage = combo2Dam;
            dm.comboStage = 2;
        }
        else
        {
            dm.currentDamage = combo3Dam;
            dm.comboStage = 3;
        }
    }
}
