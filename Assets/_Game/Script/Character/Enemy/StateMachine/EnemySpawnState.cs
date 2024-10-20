using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.isVincible = true;
        enemy.currentSpawnTime = enemy.spawnDuration;
        enemy.MaterialAppear();
    }

    public override void ExitState(Enemy enemy)
    {
        enemy.isVincible = false;
    }

    public override void UpdateState(Enemy enemy)
    {
        enemy.currentSpawnTime -= Time.deltaTime;
        if (enemy.currentSpawnTime <= 0)
        {
            enemy.collider.enabled = true;
            enemy.SwitchToState(enemy.NormalState);
        }
    }
}
