using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip playerHitSound, playerWalkSound, playerJumpSound, playerSlideSound, playerHitReactSound, enemyHitReactSound, enemyDieSound;
    static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("hitSound");
        playerWalkSound = Resources.Load<AudioClip>("walkSound");
        playerJumpSound = Resources.Load<AudioClip>("jumpSound");
        playerSlideSound = Resources.Load<AudioClip>("slideSound");
        playerHitReactSound = Resources.Load<AudioClip>("hitReactSound");
        enemyHitReactSound = Resources.Load<AudioClip>("enemyHitReactSound");
        enemyDieSound = Resources.Load<AudioClip>("enemyDieSound");


        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "hitSound":
                audioSrc.PlayOneShot(playerHitSound);
                break;

            case "walkSound":
                audioSrc.PlayOneShot(playerWalkSound);
                break;

            case "jumpSound":
                audioSrc.PlayOneShot(playerJumpSound);
                break;

            case "slideSound":
                audioSrc.PlayOneShot(playerSlideSound);
                break;

            case "hitReactSound":
                audioSrc.PlayOneShot(playerHitReactSound);
                break;

            case "enemyHitReactSound":
                audioSrc.PlayOneShot(enemyHitReactSound);
                break;

            case "enemyDieSound":
                audioSrc.PlayOneShot(enemyHitReactSound);
                break;
        }

    }

}
