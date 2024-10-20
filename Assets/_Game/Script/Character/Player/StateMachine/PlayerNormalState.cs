using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerNormalState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        
    }

    public override void UpdateState(PlayerController player)
    {
        Move(player);
    }

    public override void ExitState(PlayerController player)
    {
        
    }

    private void Move(PlayerController player)
    {
        if (player.characterController.isGrounded && player.input.spaceKeyDown)
        {
            //player.movementVelocity = Vector3.zero;
            player.SwitchToState(player.JumpState);

            //return;
        }

        if (player.characterController.isGrounded && player.input.isInputSlide)
        {
            if (player.energyToSlide <= player.GetCurrentEnergyAmount())
            {
                player.isVincible = true;

                player.SwitchToState(player.SlideState);

                return;
            }
        }

        if (player.characterController.isGrounded && player.input.mouseLeftButtonDown)
        {  
            player.SwitchToState(player.AttackState);

            return;
        }
       

        if (player.characterController.isGrounded && player.input.mouseRightButtonDown && player.canHeavyAttack)
        {
            if (player.energyToHeavyAttack <= player.GetCurrentEnergyAmount())
            {
                player.animator.SetBool("isHoldHeavyAttack", true);

                player.movementVelocity = Vector3.zero;

                player.currentHeavyAttackTime -= Time.deltaTime;

                if (player.currentHeavyAttackTime <= 0 && player.canHeavyAttack)
                {
                    player.SwitchToState(player.HeavyAttackState);

                    return;
                }

                return;
            }
        }
        else
        {
            player.animator.SetBool("isHoldHeavyAttack", false);

            player.currentHeavyAttackTime = player.timeToHeavyAttack;
            if (player.input.mouseRightButtonUp)
            {
                player.canHeavyAttack = true;
            }
        }

        player.movementVelocity.Set(player.input.inputHorizontal, 0f, player.input.inputVertical);

        //Animation Normal
        player.animator.SetFloat("Speed", player.movementVelocity.magnitude);

        player.movementVelocity.Normalize();

        if (player.movementVelocity == Vector3.zero)
        {
            return;
        }

        if (player.movementVelocity != Vector3.zero)
        {
            player._targetRotation = Mathf.Atan2(player.movementVelocity.x, player.movementVelocity.z) * Mathf.Rad2Deg +
                              player._mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, player._targetRotation, ref player._rotationVelocity,
                player.RotationSmoothTime);

            // rotate to face input direction relative to camera position
            player.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        player.movementVelocity = Quaternion.Euler(0.0f, player._targetRotation, 0.0f) * Vector3.forward;
        player.movementVelocity *= player.moveSpeed * Time.deltaTime;

        // move the player
        //player.characterController.Move(player.movementVelocity * player.moveSpeed * Time.deltaTime +
        //new Vector3(0.0f, player._verticalVelocity, 0.0f) * Time.deltaTime);

        //player.movementVelocity *= player.moveSpeed * Time.deltaTime;

        /*
        if (player.movementVelocity != Vector3.zero)
        {
            //player.transform.rotation = Quaternion.LookRotation(player.movementVelocity);
            player.visual.transform.rotation = Quaternion.LookRotation(player.movementVelocity);
        }
        */

        //Fall Animation
        player.animator.SetBool("Fall", !player.characterController.isGrounded);

        /*
        if (!player.characterController.isGrounded)
        {
            player.verticalVelocity = player.gravity;
        }
        else
        {
            player.verticalVelocity = player.gravity * 0.3f;
        }

        player.movementVelocity += player.verticalVelocity * Vector3.up * Time.deltaTime;

        player.characterController.Move(player.movementVelocity);
        */
    }
}
