using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbPatrolBehavior : EnemyBehavior
{
    //Simple patrol AI that marches back and forth between the X coordinates of any number of points
    //Enemy does not manually attack and instead relies on contact damage to hurt the player

    //Patrol Stuff

    [SerializeField]
    private List<Transform> patrolPoints;
    private int currentIndex = 0;
    private Transform targetPos;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float minStoppignDistance = 0.5f;

    public void Awake()
    {
        targetPos = patrolPoints[0];
        currentIndex = 0;
    }


    public void FixedUpdate()
    {
        if(!busy && !coolingDown)
        {
            MoveToTarget();
        }
        else if (coolingDown && ! busy)
        {
            controller.playAnim("idle");
        }
    }

    private void MoveToTarget()
    {
        float diff = 0;
        if(transform.position.x >= targetPos.position.x)
        {
            diff = transform.position.x - targetPos.position.x;
        }
        else if(transform.position.x <= targetPos.position.x)
        {
            diff = targetPos.position.x - transform.position.x;
        }


        if(diff > minStoppignDistance)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(targetPos.position.x, transform.position.y), moveSpeed));
            controller.playAnim("walk");
        }
        else
        {
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(coolDownTime);
        nextTarget();
        controller.FaceTowards(targetPos);
        coolingDown = false;

    }

    private void nextTarget()
    {
        currentIndex++;
        if(currentIndex >= patrolPoints.Count)
        {
            currentIndex = 0;
        }
        targetPos = patrolPoints[currentIndex];
    }



}
