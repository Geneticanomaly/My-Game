using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    public GameObject objectToSpawn;
    /* public GameObject groundEffect; */

    public float objectToSpawnYOffset = 0f;
    /* public float groundEffectYOffset = 0f; */

    private int activeObjectCount = 0;
    public int maxActiveObjects = 30;

    public float initialDelay = 2.0f;

    public bool isTimer;
    public float timeToSpawn;
    private float currentTimeToSpawn;

    public bool isRandomized;

    private AudioSource audioSource;
    public AudioClip[] zombieSounds;

    private bool canPlaySound = true;
    private float soundCooldown = 3.5f;

    void Start()
    {
        currentTimeToSpawn = timeToSpawn;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            UpdateTimer();
            PlayZombieSound();
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
            if (activeObjectCount < maxActiveObjects)
            {
                SpawnObject();
                activeObjectCount++;
            }
            
            currentTimeToSpawn = timeToSpawn;
        }
    }

    public void SpawnObject()
    {
        UnityEngine.AI.NavMeshHit hit;
        Vector3 randomSpawnPosition;
        float minDistanceToSpawner = 5.0f;

        // Attempt to find a clear spawn position (maximum of 10 tries)
        for (int i = 0; i < 10; i++)
        {
            // Generate a random point within the bounds of the NavMesh
            randomSpawnPosition = GetRandomNavMeshPoint();

            if (UnityEngine.AI.NavMesh.SamplePosition(randomSpawnPosition, out hit, 10.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                // The random position is on the NavMesh
                randomSpawnPosition = hit.position;

                float distanceToSpawner = Vector3.Distance(randomSpawnPosition, transform.position);

                // Use SphereCast to check for obstructions around the randomSpawnPosition
                bool leftIsClear = !Physics.Raycast(randomSpawnPosition, Vector3.left, 2.0f);
                bool rightIsClear = !Physics.Raycast(randomSpawnPosition, Vector3.right, 2.0f);
                bool forwardIsClear = !Physics.Raycast(randomSpawnPosition, Vector3.forward, 2.0f);
                bool backIsClear = !Physics.Raycast(randomSpawnPosition, Vector3.back, 2.0f);

                if (distanceToSpawner >= minDistanceToSpawner && leftIsClear && rightIsClear && forwardIsClear && backIsClear)
                {
                    // No obstructions found, spawn the objects
                    Vector3 objectToSpawnPosition = randomSpawnPosition + new Vector3(0, objectToSpawnYOffset, 0);
                    /* Vector3 groundEffectPosition = randomSpawnPosition + new Vector3(0, groundEffectYOffset, 0); */

                    Instantiate(objectToSpawn, objectToSpawnPosition, transform.rotation);
                    break;
                }
            }
        }
    }

    // Function to get a random point within the bounds of the NavMesh
    private Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomPoint = Vector3.zero;
        for (int i = 0; i < 10; i++)
        {
            // Generate a random point within the desired area
            float randomX = Random.Range(-60, 60);
            float randomZ = Random.Range(-60, 60);
            randomPoint = new Vector3(randomX, 0.0f, randomZ);

            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 10.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                // The random position is on the NavMesh
                return hit.position;
            }
        }

        // If no valid position is found, return the origin (0,0,0) as a fallback
        return Vector3.zero;
    }

    public void DecrementActiveObjectCount()
    {
        activeObjectCount--;
    }

    private void PlayZombieSound()
    {
        if (canPlaySound)
        {
            // Play a random splatter sound
            if (zombieSounds.Length > 0)
            {
                audioSource.volume = 0.5f;
                audioSource.pitch = 0.8f;
                int randomIndex = Random.Range(0, zombieSounds.Length);
                audioSource.clip = zombieSounds[randomIndex];
                audioSource.PlayOneShot(audioSource.clip);
            }

            canPlaySound = false;
            StartCoroutine(ResetSoundCooldown());
        }
    }

    private IEnumerator ResetSoundCooldown()
    {
        yield return new WaitForSeconds(soundCooldown);
        canPlaySound = true;
    }
}


