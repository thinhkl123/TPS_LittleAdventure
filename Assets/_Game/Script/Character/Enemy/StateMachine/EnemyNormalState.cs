using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        
    }

    public override void ExitState(Enemy enemy)
    {
        
    }

    public override void UpdateState(Enemy enemy)
    {
        CalculateMove(enemy);
    }

    private void CalculateMove(Enemy enemy)
    {
        enemy.agent.enabled = true;

        if (Vector3.Distance(enemy.transform.position, enemy.target.position) > enemy.agent.stoppingDistance)
        {
            enemy.agent.SetDestination(enemy.target.position);
            enemy.animator.SetFloat("Speed", 0.2f);
        }
        else
        {
            enemy.agent.SetDestination(enemy.transform.position);
            enemy.animator.SetFloat("Speed", 0f);
            if (!enemy.characterTarget.isDead && enemy.canAttack)
            {
                enemy.SwitchToState(enemy.AttackState);
            }
        }
    }
}
