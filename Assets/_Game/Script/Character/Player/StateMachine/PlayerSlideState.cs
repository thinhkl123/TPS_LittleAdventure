using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerBaseState
{
    private Vector3 slideVector;
    public override void EnterState(PlayerController player)
    {
        SoundManager.Ins.Play("Slide");
        player.canIncreaseEnergy = false;
        player.UpdateEnergyAmount(player.GetCurrentEnergyAmount() - player.energyToSlide);
        player.animator.SetTrigger("Slide");
        player.isVincible = true;

        /*
        if (player.input.inputHorizontal == 0 &&  player.input.inputVertical == 0)
        {
            slideVector = player.transform.forward;
        }
        else
        {
            slideVector.Set(player.input.inputHorizontal, 0f, player.input.inputVertical);

            player._targetRotation = Mathf.Atan2(player.movementVelocity.x, player.movementVelocity.z) * Mathf.Rad2Deg +
                              player._mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, player._targetRotation, ref player._rotationVelocity,
                player.RotationSmoothTime);

            // rotate to face input direction relative to camera position
            player.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

            slideVector = Quaternion.Euler(0.0f, player._targetRotation, 0.0f) * Vector3.forward;
        }
        */
    }
    public override void UpdateState(PlayerController player)
    {
        player.movementVelocity = player.slideSpeed * player.transform.forward * Time.deltaTime;   
    }

    public override void ExitState(PlayerController player)
    {
        player.isVincible = false;
        player.canIncreaseEnergy = true;
    }

}
