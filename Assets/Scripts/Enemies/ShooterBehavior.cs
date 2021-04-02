using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehavior : EnemyBehavior
{

    //PROJECTILE STUFF
    [SerializeField]
    private GameObject projectileObj;

    [SerializeField]
    private float projectileVelocity;

    [SerializeField]
    private Transform firingPoint;

    [SerializeField]
    private bool projectileDefaultLeft = false;

    void FixedUpdate()
    {
        if (aggro && !busy && !coolingDown)
        {
            int chance = Random.Range(0, 2);
            if(chance == 0 || coolDown)
            {
                //idle
                StartCoroutine(CoolDown());
            }
            else
            {
                //fire
                Fire();

            }
        }
    }

    IEnumerator CoolDown()
    {
        //TODO: move idling behavior to controller
        coolingDown = true;
        Debug.Log("Idling");
        controller.playAnim("wizIdle");
        yield return new WaitForSeconds(coolDownTime);
        coolingDown = false;
        coolDown = false;
    }

    void Fire()
    {
        if (target)
        {
            controller.FaceTowards(target.transform);
        }
        coolDown = true;
        busy = true;
        //perform attack
        Debug.Log("firing");
        controller.playAnimWithCancels("wizFire");
    }


    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectileObj, firingPoint);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = controller.forwardDir * projectileVelocity;
        projectile.transform.parent = null;
    }

    public void returnToIdle()
    {
        if (!busy)
        {
            controller.playAnim("wizIdle");
        }
    }
}
