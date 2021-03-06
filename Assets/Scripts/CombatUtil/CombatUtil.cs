using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUtil : MonoBehaviour
{
    [SerializeField]
    private bool isStopped;

    public void hitStop(float duration)
    {
        if (isStopped)
        {
            return;
        }
        else
        {
            Time.timeScale = 0f;
            StartCoroutine(StopEffect(duration));
        }
    }

    IEnumerator StopEffect(float duration)
    {
        isStopped = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        isStopped = false;
    }
}
