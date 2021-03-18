using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Health Component Interface used on all characters with health
 */
public interface IHealthManager 
{
    /*
     * Kill is generic method used for standard deaths on any character with health
     */
    void Kill();

    /*
     * KillQuietly is used to kill when you don't want any extra effects (i.e when falling into a bottomless pit)
     */
    void KillQuietly();

    /*
     * DealDamage is called by hurtbox object. This should keep things such as invincibility time in mind.
     */
    void DealDamage(float dam, float hitstun, int comboStage);

}
