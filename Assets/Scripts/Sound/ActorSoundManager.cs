using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ActorSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip takeDamageSound, dieSound, moveSound, jumpSound, castSound, attack1Sound, attack2Sound, attack3Sound;

    [SerializeField]
    protected AudioSource src;


    public virtual void PlayEffect(string effectName)
    {
        switch (effectName)
        {
            case "dam":
                src.clip = takeDamageSound;
                src.Play();
                break;

            case "die":
                src.PlayOneShot(dieSound);
                break;
            case "cast":
                src.PlayOneShot(castSound);
                break;
            case "attack1":
                src.clip = attack1Sound;
                src.Play();
                break;
            case "attack2":
                src.clip = attack2Sound;
                src.Play();
                break;
            case "attack3":
                src.clip = attack3Sound;
                src.Play();
                break;
        }
    }


}
