using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField] private VisualEffect footStepVFX;
    [SerializeField] private ParticleSystem blade01;
    [SerializeField] private ParticleSystem blade02;
    [SerializeField] private ParticleSystem blade03;
    [SerializeField] private VisualEffect slash;
    [SerializeField] private VisualEffect healVFX;

    public void UpdateFootStepVFX(bool isRunning)
    {
        if (isRunning)
            footStepVFX.Play();
        else 
            footStepVFX.Stop();
    }

    public void PlayBlade01()
    {
        blade01.Play();
    }

    public void PlayBlade02()
    {
        blade02.Play();
    }

    public void PlayBlade03()
    {
        blade03.Play();
    }

    public void StopBlade()
    {
        blade01.Simulate(0);
        blade01.Stop();

        blade02.Simulate(0);
        blade02.Stop();

        blade03.Simulate(0);
        blade03.Stop();
    }

    public void PlaySlash(Vector3 pos)
    {
        slash.transform.position = pos;
        slash.Play();
    }

    public void PlayHealVFX()
    {
        healVFX.Play();
    }
}
