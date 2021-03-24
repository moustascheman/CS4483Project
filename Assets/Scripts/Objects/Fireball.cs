using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private int comboStage;
    public void OnTriggerEnter2D(Collider2D col)
    {
        Hurtbox hurtbox = col.GetComponent<Hurtbox>();
        if (hurtbox)
        {
            if (hurtbox.isPlayerHurtbox)
            {
                hurtbox.detectHit(damage, 0f, comboStage);
                Destroy(gameObject);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}
