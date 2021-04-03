using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{


    [SerializeField]
    private DamageSource dm;

    [SerializeField]
    private PlayerMovement pm;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private ActorSoundManager sm;

    public int stage = 0;

    //Can Cancel during the recovery frames of an attack
    private bool canCancel = false;


    //busy when an attack is playing or in a state where they cannot attack (such as hitstun)
    public bool isBusy = false;


    public void OnAttack()
    {
        if (!isBusy || canCancel)
        {
            isBusy = true;
            bool groundState = pm.getGroundedState();
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

        pm.updateAnimationAllowed = false;
        pm.movementEnabled = false;

        pm.IsDashing = false;

        if (stage == 0)
        {
            //first hit
            stage++;
            UpdateDamageManagerSettings();
            pm.resetAnimatonState();
            pm.changeAnimationState(PlayerAnimStates.ATTACK1_START);
            sm.PlayEffect("attack1");
        }
        else if (stage == 1)
        {
            //second hit
            stage++;
            UpdateDamageManagerSettings();
            pm.changeAnimationState(PlayerAnimStates.ATTACK2_START);
            sm.PlayEffect("attack2");
        }
        else if (stage == 2)
        {
            stage++;
            UpdateDamageManagerSettings();
            pm.changeAnimationState(PlayerAnimStates.ATTACK3_START);
            sm.PlayEffect("attack3");
        }
    }

    private void jumpAttack()
    {
        pm.updateAnimationAllowed = false;
        //Attack properties
        dm.currentDamage = 5f;
        dm.currentHitstun = 2f;
        dm.comboStage = 3;

        pm.changeAnimationState(PlayerAnimStates.JUMP_ATTACK);
        sm.PlayEffect("attack1");

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
        canCancel = true;
    }

    public void endJumpAttack()
    {
        stage = 0;
        pm.updateAnimationAllowed = true;
        isBusy = false;
    }

    public void resetAttackAnim()
    {
        canCancel = false;
        stage = 0;
        pm.updateAnimationAllowed = true;
        pm.movementEnabled = true;
        isBusy = false;
    }



    public void endChain()
    {
        resetAttackAnim();
    }

    public void stunPlayer()
    {
        anim.Play(PlayerAnimStates.DAMAGE_ANIM, -1, 0f);
        isBusy = true;
        canCancel = false;
        stage = 0;
        pm.stunPlayer();
    }

    public void endStun()
    {
        isBusy = false;
        resetAttackAnim();
        pm.endStun();
    }

}
