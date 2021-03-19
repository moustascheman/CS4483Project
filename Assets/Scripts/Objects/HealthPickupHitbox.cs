using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupHitbox : MonoBehaviour
{
    [SerializeField]
    private bool isPlayerHitbox = false;

    [SerializeField]
    private DamageSource dam;

    public void OnTriggerEnter2D(Collider2D col)
    {
        Hurtbox hurtbox = col.GetComponent<Hurtbox>();
        if (hurtbox)
        {
            if (hurtbox.isPlayerHurtbox)
            {
                hurtbox.detectHit(dam.currentDamage, dam.currentHitstun, dam.comboStage);
                Destroy(dam.gameObject);
            }
        }
    }
}
