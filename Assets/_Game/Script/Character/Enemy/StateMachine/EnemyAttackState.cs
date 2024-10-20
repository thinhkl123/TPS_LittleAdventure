using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animator.SetTrigger("Attack");
        enemy.currentDamage = enemy.attackDamage;

        enemy.CoolDownTimeToAttack();
        
        RotateToPlayer(enemy);
    }

    public override void ExitState(Enemy enemy)
    {
        
    }

    public override void UpdateState(Enemy enemy)
    {
        if (enemy.enemyType == Enemy.EnemyType.Archer && enemy.isTargeting)
        {
            RotateToPlayer(enemy);
        }
    }

    private void RotateToPlayer(Enemy enemy)
    {
        Vector3 dir = enemy.target.position - enemy.transform.position;

        enemy.transform.rotation = Quaternion.LookRotation(dir);
    }

    
}
