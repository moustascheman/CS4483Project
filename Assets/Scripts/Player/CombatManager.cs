using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager cbmInstance;

    [SerializeField]
    private PlayerMovement pm;

    [SerializeField]
    private Animator anim;

    public bool canReceiveInput = true;
    public bool inputReceived;

    public int stage = 0;

    [SerializeField]
    private BoxCollider2D attackHitbox;

    [SerializeField]
    private LayerMask hitboxLayer;

    [SerializeField]
    private CombatUtil cUtil;


    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        cbmInstance = this;
    }

    public void OnAttack()
    {
        bool groundState = pm.getGroundedState();
        if (groundState)
        {
            pm.movementEnabled = false;
            pm.isAttacking = true;
            regularAttack();
        }
        else
        {
            pm.isAttacking = true;
            jumpAttack();
        }
    }

    private void regularAttack()
    {
        
        if (canReceiveInput)
        {
            inputReceived = true;
            canReceiveInput = false;
        
            if(stage == 0)
            {
                //first hit
                stage++;
                anim.Play("attack1Startup");
            }
            else if(stage == 1)
            {
                //second hit
                stage++;
                anim.Play("attack2Active");
            }
            else if(stage == 2)
            {
                print("NEWTESt");
                stage++;
                anim.Play("attack3Startup");
            }
        }
        else
        {
            return;
        }
    }

    private void jumpAttack()
    {
        if (canReceiveInput)
        {
            canReceiveInput = false;
            anim.Play("jumpAttackStartup");

        }
    }

    public void disableAttacking()
    {
        //use when hit or need to disable attacking
        stage = 0;
        canReceiveInput = false;
    }

    public void allowAttackCancel()
    {
        
        canReceiveInput = true;
    }

    public void endJumpAttack()
    {
        
        canReceiveInput = true;
        pm.endJumpEarly();
        pm.isAttacking = false;
        pm.resetAnimatonState();
    }

    public void resetAttackAnim()
    {
        print(stage);
        stage = 0;
        canReceiveInput = true;
        pm.isAttacking = false;
        pm.movementEnabled = true;
        pm.IsDashing = false;
        pm.resetAnimatonState();
    }

    public void PerformAttack(float damage)
    {
        Collider2D hit = Physics2D.OverlapBox(attackHitbox.bounds.center, attackHitbox.bounds.size, 0f, hitboxLayer);
        if (hit) { 
            GameObject hitObj = hit.gameObject.transform.parent.gameObject;
            cUtil.hitStop(0.05f);
        }
    }


}
