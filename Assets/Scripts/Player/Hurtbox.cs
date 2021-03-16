using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public IHealthManager healthMan;
    public bool isPlayerHurtbox = false;

    public void Awake()
    {
        healthMan = GetComponentInParent<IHealthManager>();
    }

    public virtual void detectHit(float dam, float hitstun, int stage)
    {
        Debug.Log("HIT");
        healthMan.DealDamage(dam, hitstun, stage);
    }
}
