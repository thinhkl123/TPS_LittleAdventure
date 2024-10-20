using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animator.SetTrigger("Dead");
        enemy.isDead = true;
        enemy.agent.enabled = false;
        enemy.MaterialDissolve();
        SkinnedMeshRenderer mesh = enemy.GetComponentInChildren<SkinnedMeshRenderer>();
        mesh.gameObject.layer = 0;
        enemy.healthSlider.gameObject.SetActive(false);
        EnemyGroup.Ins.RemoveEnemy(enemy);
    }

    public override void ExitState(Enemy enemy)
    {
        
    }

    public override void UpdateState(Enemy enemy)
    {
        
    }
}
