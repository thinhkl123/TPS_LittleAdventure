using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.animator.SetTrigger("Jump");
        player.isJumping = true;
    }

    public override void UpdateState(PlayerController player)
    {
        Vector3 velocity = Vector3.zero;

        velocity.y = Mathf.Sqrt(player.jumpHeight * -2f * player.gravity);

        player.movementVelocity = velocity * Time.deltaTime;
    }

    public override void ExitState(PlayerController player)
    {
        player.isJumping = false;
    }
}
