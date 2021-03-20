using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroRange : MonoBehaviour
{
    [SerializeField]
    private EnemyBehavior behavior;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //set aggro on
            behavior.aggro = true;
            behavior.target = col.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //set aggro off
            behavior.target = null;
            behavior.aggro = false;

        }
    }



}
