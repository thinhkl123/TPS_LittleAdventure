using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints;
    public List<Gate> gates;

    public bool isLastLevel;
    private bool hasSpawned;
    private bool isFinished;

    private void Awake()
    {
        hasSpawned = false;
    }

    private void Start()
    {
        EnemyGroup.Ins.OnLevelCompleted += EnemyGroup_OnLevelCompleted;
    }

    private void EnemyGroup_OnLevelCompleted(object sender, System.EventArgs e)
    {
        if (!hasSpawned || isFinished)
        {
            return;
        }

        isFinished = true;

        if (isLastLevel)
        {
            GameManager.Ins.WinGame();

            return;
        }

        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].Open();
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Enemy enemyGO = Instantiate(spawnPoints[i].enemyPrefab, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation);
            
            EnemyGroup.Ins.AddEnemy(enemyGO);
        }

        hasSpawned = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasSpawned)
            {
                SpawnEnemy();
            }
        }
    }
}
