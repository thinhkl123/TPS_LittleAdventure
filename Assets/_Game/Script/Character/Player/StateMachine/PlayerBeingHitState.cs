using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeingHitState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.animator.SetTrigger("BeingHit");
        player.movementVelocity = Vector3.zero;
    }

    public override void ExitState(PlayerController player)
    {
        
    }

    public override void UpdateState(PlayerController player)
    {
        
    }
}
