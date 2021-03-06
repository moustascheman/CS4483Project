using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBox : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement pm;
    [SerializeField]
    private LayerMask interactLayer;

    //if the players head collides with a wall (from the top) it should end the jump
    void OnTriggerEnter2D(Collider2D col)
    {
        //check to see if on ground layer
        if((1<<col.gameObject.layer & interactLayer.value) == 1 << col.gameObject.layer)
        {
            pm.endJumpEarly();
        }
    }
}
