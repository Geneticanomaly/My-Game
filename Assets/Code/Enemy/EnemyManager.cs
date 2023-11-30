using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public LimbManager limbManager;
    public EnemySpawner enemySpawner;

     private void Awake()
    {
        Instance = this;
    }

    public void DestroyEnemies()
    {
        //Find all game objects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EnemyAILogic enemyAI = enemy.GetComponent<EnemyAILogic>();;
            limbManager.spawnLimbs(enemyAI);
            enemySpawner.DecrementActiveObjectCount();
            Destroy(enemy);
        }
    }

}
