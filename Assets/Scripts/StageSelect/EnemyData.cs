using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField]
    EnemyContorller[] _stageEnemys = default;
    [SerializeField]
    Transform[] _enemySpawnPoints = default;
    public void SpawnEnemy()
    {
        foreach (var enemy in _stageEnemys)
        {
            enemy.StartSet();
        }
    }
}
