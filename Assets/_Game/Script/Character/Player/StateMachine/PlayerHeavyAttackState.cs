using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyAttackState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        SoundManager.Ins.Play("HeavyAttack");
        player.canIncreaseEnergy = false;
        player.UpdateEnergyAmount(player.GetCurrentEnergyAmount() - player.energyToHeavyAttack);
        player.animator.SetTrigger("HeavyAttack");
        player.currentDamage = player.heavyAttackDamage;
        player.attackStartTime = Time.time;
        player.currentHeavyAttackTime = player.timeToHeavyAttack;
        player.canHeavyAttack = false;
        RotateToEnemy(player);
    }
    public override void UpdateState(PlayerController player)
    {
        player.movementVelocity = Vector3.zero;

        if (Time.time < player.attackStartTime + player.attackSlideDuration)
        {
            float timePassed = Time.time - player.attackStartTime;
            float lerpTime = timePassed / player.attackSlideDuration;
            player.movementVelocity = Vector3.Lerp(player.transform.forward * player.attackSlideSpeed, Vector3.zero, lerpTime);
        }

        player.input.mouseRightButtonDown = false;
    }

    public override void ExitState(PlayerController player)
    {
        player.canIncreaseEnergy = true;
    }

    private void RotateToEnemy(PlayerController player)
    {
        Transform target = player.enemyGroup.GetClosetEnemy(player.transform);

        if (target == null)
        {
            return;
        }

        if (Vector3.Distance(player.transform.position, target.position) <= player.maxDistanceTarget)
        {
            player.transform.rotation = Quaternion.LookRotation(target.position - player.transform.position);
        }
    }
}
