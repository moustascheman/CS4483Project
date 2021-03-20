using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatTemp : MonoBehaviour
{
    /*
    [SerializeField]
    private DamageSource dm;

    [SerializeField]
    private PlayerMovement pm;

    [SerializeField]
    private Animator anim;

    public bool canReceiveInput = true;
    public bool inputReceived;
    [SerializeField]
    private bool recovering = false;

    public int stage = 0;


    [SerializeField]
    private LayerMask hitboxLayer;

    [SerializeField]
    private CombatUtil cUtil;



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

            if (stage == 0)
            {
                //first hit
                stage++;
                UpdateDamageManagerSettings();
                anim.Play(PlayerAnimStates.ATTACK1_START);
            }
            else if (stage == 1)
            {
                //second hit
                stage++;
                UpdateDamageManagerSettings();
                anim.Play(PlayerAnimStates.ATTACK2_START);
            }
            else if (stage == 2)
            {
                stage++;
                UpdateDamageManagerSettings();
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
            //Attack properties
            dm.currentDamage = 5f;
            dm.currentHitstun = 2f;
            dm.comboStage = 3;


            canReceiveInput = false;
            anim.Play(PlayerAnimStates.JUMP_ATTACK);

        }
    }

    private void UpdateDamageManagerSettings()
    {
        if (stage == 1)
        {
            dm.currentDamage = 2f;
            dm.currentHitstun = 0.3f;
        }
        if (stage == 2)
        {
            dm.currentDamage = 4f;
            dm.currentHitstun = 0.3f;
        }
        if (stage == 3)
        {
            dm.currentDamage = 6f;
            dm.currentHitstun = 0.3f;
        }
        dm.comboStage = stage;
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

    public void stunPlayer()
    {
        pm.stunPlayer();
        resetAttackAnim();
        canReceiveInput = false;
        recovering = true;
    }

    public void endStun()
    {
        pm.endStun();
        canReceiveInput = true;
        recovering = false;
    }

    */
    [SerializeField]
    private DamageSource dm;

    [SerializeField]
    private PlayerMovement pm;

    [SerializeField]
    private Animator anim;

    public int stage = 0;



    //busy when in a state where animations cannot be cancelled
    //these situations are hitstun, death, startup and active attack frames
    public bool isBusy = false;


    public void OnAttack()
    {
        if (!isBusy)
        {
            isBusy = true;
            bool groundState = pm.getGroundedState();
            pm.updateAnimationAllowed = false;
            if (groundState)
            {
                regularAttack();
            }
            else
            {
                jumpAttack();
            }
        }
    }

    private void regularAttack()
    {
        pm.movementEnabled = false;
        pm.IsDashing = false;

        if (stage == 0)
        {
            //first hit
            stage++;
            UpdateDamageManagerSettings();
            pm.changeAnimationState(PlayerAnimStates.ATTACK1_START);
        }
        else if (stage == 1)
        {
            //second hit
            stage++;
            UpdateDamageManagerSettings();
            pm.changeAnimationState(PlayerAnimStates.ATTACK2_START);
        }
        else if (stage == 2)
        {
            stage++;
            UpdateDamageManagerSettings();
            pm.changeAnimationState(PlayerAnimStates.ATTACK3_START);
        }
    }

    private void jumpAttack()
    {
 
        //Attack properties
        dm.currentDamage = 5f;
        dm.currentHitstun = 2f;
        dm.comboStage = 3;

        anim.Play(PlayerAnimStates.JUMP_ATTACK);

    }

    private void UpdateDamageManagerSettings()
    {
        if (stage == 1)
        {
            dm.currentDamage = 2f;
            dm.currentHitstun = 0f;
        }
        if (stage == 2)
        {
            dm.currentDamage = 4f;
            dm.currentHitstun = 0f;
        }
        if (stage == 3)
        {
            dm.currentDamage = 6f;
            dm.currentHitstun = 0f;
        }
        dm.comboStage = stage;
    }


    public void allowAttackCancel()
    {
        isBusy = false;
    }

    public void endJumpAttack()
    {

        pm.endJumpEarly();
        pm.resetAnimatonState();
    }

    public void resetAttackAnim()
    {
        isBusy = false;
        pm.updateAnimationAllowed = true;
        pm.movementEnabled = true;
    }

    public void stunPlayer()
    {
        isBusy = true;
        pm.stunPlayer();
        resetAttackAnim();
    }

    public void endStun()
    {
        isBusy = false;
        pm.endStun();
    }
}
