using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public PlayerController player;
    public PlayerVFXManager vFXManager;
    public DamageCaster damageCaster;

    public void EndJumpAnimation()
    {
        player.SwitchToState(player.NormalState);
    }

    public void EndAttackAnimation()
    {
        player.SwitchToState(player.NormalState);
    }

    public void EndSlideAnimation()
    {
        player.SwitchToState(player.NormalState);
    }

    public void EndBeingHitAnimation()
    {
        player.SwitchToState(player.NormalState);
    }

    public void EnableDamageCaster()
    {
        damageCaster.EnableDamageCaster();
    }

    public void DisableDamageCaster()
    {
        damageCaster.DisableDamageCaster();
    }
}
