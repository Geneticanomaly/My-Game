using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject groundEffect;
    public GameObject flyingParticles;

    public float objectToSpawnYOffset = 1.2f;
    public float groundEffectYOffset = 0f;

    public bool isTimer;
    public float timeToSpawn;
    private float currentTimeToSpawn;

    private int randomSeed = 1000;

    void Start()
    {
        currentTimeToSpawn = timeToSpawn;
        Random.InitState(randomSeed);
    }

    void Update()
    {
        if (isTimer)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            currentTimeToSpawn = timeToSpawn;
        }
    }

    public void SpawnObject()
    {
        UnityEngine.AI.NavMeshHit hit;
        Vector3 randomSpawnPosition;
        float minDistanceToSpawner = 2.0f;

        for (int i = 0; i < 10; i++)
        {
            randomSpawnPosition = GetRandomNavMeshPoint();

            if (UnityEngine.AI.NavMesh.SamplePosition(randomSpawnPosition, out hit, 10.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                // The random position is on the NavMesh
                randomSpawnPosition = hit.position;

                float distanceToSpawner = Vector3.Distance(randomSpawnPosition, transform.position);

                bool leftIsClear = !Physics.Raycast(randomSpawnPosition, Vector3.left, 5.0f);
                bool rightIsClear = !Physics.Raycast(randomSpawnPosition, Vector3.right, 5.0f);
                bool forwardIsClear = !Physics.Raycast(randomSpawnPosition, Vector3.forward, 5.0f);
                bool backIsClear = !Physics.Raycast(randomSpawnPosition, Vector3.back, 5.0f);

                if (distanceToSpawner >= minDistanceToSpawner && leftIsClear && rightIsClear && forwardIsClear && backIsClear)
                {
                    Vector3 objectToSpawnPosition = randomSpawnPosition + new Vector3(0, objectToSpawnYOffset, 0);
                    Vector3 groundEffectPosition = randomSpawnPosition + new Vector3(0, groundEffectYOffset, 0);

                    Instantiate(objectToSpawn, objectToSpawnPosition, Quaternion.identity);

                    Instantiate(groundEffect, groundEffectPosition, Quaternion.identity);

                    Instantiate(flyingParticles, objectToSpawnPosition, Quaternion.identity);

                    break;
                }
            }
        }
    }  

    private Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomPoint = Vector3.zero;
        UnityEngine.AI.NavMeshHit hit;

        // Get a random point
        if (UnityEngine.AI.NavMesh.SamplePosition(
            new Vector3(Random.Range(-60f, 60f), 0f, Random.Range(-60f, 60f)),
            out hit,
            10f, // Distance
            UnityEngine.AI.NavMesh.AllAreas)) 
        {
            randomPoint = hit.position;
        }

        return randomPoint;
    }

}
