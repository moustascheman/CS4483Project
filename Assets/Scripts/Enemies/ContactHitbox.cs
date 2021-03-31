using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactHitbox : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private int comboStage;

    public void OnTriggerStay2D(Collider2D col)
    {
        Hurtbox hurtbox = col.GetComponent<Hurtbox>();
        if (hurtbox)
        {
            if (hurtbox.isPlayerHurtbox)
            {
                hurtbox.detectHit(damage, 0f, comboStage);
                
            }
        }
    }
}
