using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private LayerMask hurtboxLayer;

    [SerializeField]
    private bool isPlayerHitbox = false;

    [SerializeField]
    private DamageSource dam;

    public void OnTriggerEnter2D(Collider2D col)
    {
        Hurtbox hurtbox = col.GetComponent<Hurtbox>();
        if (hurtbox)
        {
            if ((isPlayerHitbox && !hurtbox.isPlayerHurtbox) || (!isPlayerHitbox && hurtbox.isPlayerHurtbox))
            {
                hurtbox.detectHit(dam.currentDamage, dam.currentHitstun, dam.comboStage);
            }
        }
    }

}
