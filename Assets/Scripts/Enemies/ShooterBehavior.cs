using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehavior : MonoBehaviour
{

    public bool aggro = false;
    private bool coolDown = false;
    private bool coolingDown = false;
    public bool busy = false;
    [SerializeField]
    private float coolDownTime = 3f;
    [SerializeField]
    EnemyController controller;
    public GameObject target = null;

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
        controller.playAnim("wizFire");
    }


    public void endAttackAnim()
    {
        busy = false;
    }
}
