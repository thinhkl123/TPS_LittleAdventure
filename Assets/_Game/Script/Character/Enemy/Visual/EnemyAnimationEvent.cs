using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public Enemy enemy;
    public DamageCaster damageCaster;

    public void EndAttackAnimation()
    {
        enemy.SwitchToState(enemy.NormalState);
    }

    public void EnableDamageCaster()
    {
        if (damageCaster != null)
        {
            damageCaster.EnableDamageCaster();
        }
    }

    public void DisableDamageCaster()
    {
        if (damageCaster != null)
        {
            damageCaster.DisableDamageCaster();
        }
    }

    public void Shoot()
    {
        enemy.Shoot();
    }

    public void StartTargetToPlayer()
    {
        enemy.isTargeting = true;
    }

    public void StopTargetToPlayer()
    {
        enemy.isTargeting = false;
    }

    public void PlayMeleeAttackSound()
    {
        SoundManager.Ins.Play("EnemyMeleeAttack");
    }
    
    public void PlayArcherAttackSound()
    {
        SoundManager.Ins.Play("EnemyArcherAttack");
    }


}
