using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public bool aggro = false;
    protected bool coolDown = false;
    protected bool coolingDown = false;
    public bool busy = false;
    [SerializeField]
    protected float coolDownTime = 3f;
    [SerializeField]
    protected EnemyController controller;
    public GameObject target = null;

    public void endAttackAnim()
    {
        busy = false;
    }

}
