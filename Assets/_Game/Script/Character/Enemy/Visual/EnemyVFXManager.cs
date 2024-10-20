using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFXManager : MonoBehaviour
{
    [SerializeField] private VisualEffect footStepVFX;
    [SerializeField] private VisualEffect attackVFX;
    public ParticleSystem beinghitVFX;
    public VisualEffect beingHitSplashVFX;

    public void UpdateFootStepVFX(bool isRunning)
    {

        if (isRunning)
            footStepVFX.Play();
        else
            footStepVFX.Stop();
    }

    public void PlayFootStepVFX()
    {
        footStepVFX.SendEvent("OnPlay");
    }

    public void PlayAttackVFX()
    {
        attackVFX.SendEvent("OnPlay");
    }

    public void PlayBeingHitVFX(Vector3 attackerPos)
    {
        Vector3 forceForward = transform.position - attackerPos;
        forceForward.Normalize();
        forceForward.y = 0;
        beinghitVFX.transform.rotation = Quaternion.LookRotation(forceForward);
        beinghitVFX.Play();

        Vector3 splashPos = transform.position;
        splashPos.y += 2f;
        VisualEffect newSplashVFX = Instantiate(beingHitSplashVFX, splashPos, Quaternion.identity);
        beingHitSplashVFX.SendEvent("OnPlay");
        Destroy(newSplashVFX.gameObject, 10f);
    }
}
