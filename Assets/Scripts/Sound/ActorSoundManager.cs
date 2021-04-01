using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ActorSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip takeDamageSound, dieSound, moveSound, jumpSound, castSound, attack1Sound, attack2Sound, attack3Sound, dashSound;

    [SerializeField]
    protected AudioSource src;


    public virtual void PlayEffect(string effectName)
    {
        switch (effectName)
        {
            case "dam":
                src.PlayOneShot(takeDamageSound);
                break;

            case "die":
                src.PlayOneShot(dieSound);
                break;
            case "jump":
                src.PlayOneShot(jumpSound);
                break;
            case "cast":
                src.PlayOneShot(castSound);
                break;
            case "attack1":
                src.PlayOneShot(attack1Sound);
                break;
            case "attack2":
                src.PlayOneShot(attack2Sound);
                break;
            case "attack3":
                src.PlayOneShot(attack3Sound);
                break;
            case "dash":
                src.PlayOneShot(dashSound);
                break;
        }
    }


}
