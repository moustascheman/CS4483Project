using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    //Main menu screen button wiring to scenes
    public void tutorial()
    {
        SceneManager.LoadScene("IntroLevel");
    }

    public void garden()
    {
        SceneManager.LoadScene("GardenScene");
    }
    public void basement()
    {
        SceneManager.LoadScene("DungeonBasementScene");
    }

    public void backstage()
    {
        SceneManager.LoadScene("Backstage");
    }

    public void finalBoss(){
        SceneManager.LoadScene("FinalBossArena"); //Placeholder, change to actual final boss scene
    }
}