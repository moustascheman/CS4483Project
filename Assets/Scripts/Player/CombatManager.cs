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
    [SerializeField]
    private bool recovering = false;
    [SerializeField]
    private bool cancelling = false;

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
        if (!recovering)
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
    }

    private void regularAttack()
    {
        
        if (canReceiveInput && !recovering)
        {
            
            canReceiveInput = false;
        
            if(stage == 0)
            {
                //first hit
                stage++;
                anim.Play(PlayerAnimStates.ATTACK1_START);
            }
            else if(stage == 1)
            {
                //second hit
                stage++;
                anim.Play(PlayerAnimStates.ATTACK2_START);
            }
            else if(stage == 2)
            {
                stage++;
                anim.Play(PlayerAnimStates.ATTACK3_START);
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
            anim.Play(PlayerAnimStates.JUMP_ATTACK);

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
        canReceiveInput = false;
        recovering = true;
        stage = 0;
        
        pm.isAttacking = false;
        pm.movementEnabled = true;
        pm.IsDashing = false;
        pm.resetAnimatonState();
        canReceiveInput = true;
        recovering = false;
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
