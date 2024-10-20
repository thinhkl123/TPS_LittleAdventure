using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : Singleton<EnemyGroup>
{
    public event EventHandler OnLevelCompleted;

    public List<Enemy> enemies;

    public Transform GetClosetEnemy(Transform target)
    {
        if (enemies.Count == 0)
        {
            return null;
        }

        float minDistance = Vector3.Distance(enemies[0].transform.position, target.position);
        int indx = 0;

        for (int i=1; i<enemies.Count; i++)
        {
            float distance = Vector3.Distance(enemies[i].transform.position, target.position);
            if (distance < minDistance)
            {
                indx = i;
                minDistance = distance;
            }
        }

        return enemies[indx].transform;
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            OnLevelCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
