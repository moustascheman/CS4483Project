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


    public void playAnim(string animationName)
    {
        if (!state.Equals(animationName)) {
            state = animationName;
            anim.Play(animationName);
        }
    }

    public float getRemainingAnimTime()
    {
        
        return anim.GetCurrentAnimatorStateInfo(0).length;
    }

    public void FaceTowards(Transform target)
    {
        if (DefaultFaceDirectionIsLeft)
        {
            if(target.position.x > gameObject.transform.position.x && gameObject.transform.localScale.x != -1)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(target.position.x < gameObject.transform.position.x && gameObject.transform.localScale.x != 1)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if(target.position.x > gameObject.transform.position.x && gameObject.transform.localScale.x != 1)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if(target.position.x < gameObject.transform.position.x && gameObject.transform.localScale.x != -1)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }




}
