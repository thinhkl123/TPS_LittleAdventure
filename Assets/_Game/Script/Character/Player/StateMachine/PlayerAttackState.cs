using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        SoundManager.Ins.Play("Attack");
        player.animator.SetTrigger("Attack");
        player.currentDamage = player.attackDamage;
        player.attackStartTime = Time.time;
        RotateToEnemy(player);
    }
    public override void UpdateState(PlayerController player)
    {
        if (player.characterController.isGrounded && player.input.isInputSlide)
        {
            if (player.energyToSlide <= player.GetCurrentEnergyAmount())
            {
                player.isVincible = true;

                player.SwitchToState(player.SlideState);

                return;
            }
        }

        player.movementVelocity = Vector3.zero;

        if (Time.time < player.attackStartTime + player.attackSlideDuration)
        {
            float timePassed = Time.time - player.attackStartTime;  
            float lerpTime = timePassed / player.attackSlideDuration;
            player.movementVelocity = Vector3.Lerp(player.transform.forward * player.attackSlideSpeed, Vector3.zero, lerpTime);
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
            player.animator.SetBool("isHoldHeavyAttack", true);

            player.currentHeavyAttackTime = player.timeToHeavyAttack;
            if (player.input.mouseRightButtonUp)
            {
                player.canHeavyAttack = true;
            }
        }

        if (player.input.mouseLeftButtonDown && player.characterController.isGrounded)
        {
            string currentClipName = player.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            player.attackAnimationDuration = player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (currentClipName != "Attack02" && player.attackAnimationDuration > 0.5f &&  player.attackAnimationDuration < 0.7f)
            {
                player.SwitchToState(player.AttackState);
            }
        }

        player.input.mouseLeftButtonDown = false;
    }

    public override void ExitState(PlayerController player)
    {
        
    }

    private void RotateToEnemy(PlayerController player)
    {
        Transform target = player.enemyGroup.GetClosetEnemy(player.transform);

        if (target ==  null)
        {
            return;
        }

        if (Vector3.Distance(player.transform.position, target.position) <= player.maxDistanceTarget)
        {
            player.transform.rotation = Quaternion.LookRotation(target.position - player.transform.position);
        }
    }

}
