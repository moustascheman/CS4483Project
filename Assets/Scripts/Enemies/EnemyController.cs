using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private bool DefaultFaceDirectionIsLeft = false;
    private string state = "";

    private bool deathLock = false;

    [SerializeField]
    private EnemyBehavior bhvr;

    public Vector2 forwardDir = Vector2.left;

    public void playAnim(string animationName)
    {
        if (!state.Equals(animationName)) {
            state = animationName;
            anim.Play(animationName);
        }
    }

    public void playAnimWithCancels(string name)
    {
        state = PlayerAnimStates.WILDCARD;
        anim.Play(name, -1, 0f);
    }

    public float getRemainingAnimTime()
    {
        
        return anim.GetCurrentAnimatorStateInfo(0).length;
    }

    public void pauseBehavior()
    {
        bhvr.busy = true;
    }

    public void DeathLock()
    {
        deathLock = true;
    }

    public void unlock()
    {
        deathLock = false;
    }


    public void resumeBehavior()
    {
        if (!deathLock)
        {
            bhvr.busy = false;
        }
    }

    public void FaceTowards(Transform target)
    {
        if (DefaultFaceDirectionIsLeft)
        {
            if(target.position.x > gameObject.transform.position.x && gameObject.transform.localScale.x != -1)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                forwardDir = Vector2.right;
            }
            else if(target.position.x < gameObject.transform.position.x && gameObject.transform.localScale.x != 1)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                forwardDir = Vector2.left;
            }
        }
        else
        {
            if(target.position.x > gameObject.transform.position.x && gameObject.transform.localScale.x != 1)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                forwardDir = Vector2.right;
            }
            else if(target.position.x < gameObject.transform.position.x && gameObject.transform.localScale.x != -1)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                forwardDir = Vector2.left;
            }
        }
    }




}
