using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbManager : MonoBehaviour
{

    public GameObject headPrefab;
    public GameObject leftArmPrefab;
    public GameObject rightArmPrefab;

    public GameObject particleEffectPrefab;
    public GameObject splashEffectPrefab;

    // Start is called before the first frame update
    public void spawnLimbs(EnemyAILogic enemyAI)
    {
        Debug.Log("AM I HERE");
        Vector3 headSpawnPosition = enemyAI.UpperChestPosition + new Vector3(0, -1.0f, 0);
        Vector3 leftArmSpawnPosition = enemyAI.UpperChestPosition;
        Vector3 rightArmSpawnPosition = enemyAI.UpperChestPosition;

        /* Vector3 headSpawnPosition = enemyAI.HeadPosition + new Vector3(0, -2.0f, 0);
        Vector3 leftArmSpawnPosition = enemyAI.LeftArmPosition;
        Vector3 rightArmSpawnPosition = enemyAI.RightArmPosition; */

        // Generate random rotations
        Quaternion randomHeadRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        Quaternion randomLeftArmRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        Quaternion randomRightArmRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        // Instantiate head, left arm, and right arm with random rotations
        GameObject head = Instantiate(headPrefab, enemyAI.transform.position, randomHeadRotation);
        GameObject leftArm = Instantiate(leftArmPrefab, enemyAI.transform.position, randomLeftArmRotation);
        GameObject rightArm = Instantiate(rightArmPrefab, enemyAI.transform.position, randomRightArmRotation);
        GameObject particleEffect = Instantiate(particleEffectPrefab, enemyAI.transform.position, Quaternion.identity);
        GameObject splashEffect = Instantiate(splashEffectPrefab, enemyAI.transform.position, Quaternion.identity);

        // Add force to the spawned limbs
        ApplyForceToLimb(head);
        ApplyForceToLimb(leftArm);
        ApplyForceToLimb(rightArm);

        // Destroy the limbs after a delay
        StartCoroutine(DestroyLimbs(head, leftArm, rightArm, 4.0f));
        StartCoroutine(DestroyParticleEffects(particleEffect, splashEffect, 4.0f));
    }
        
    

    private void ApplyForceToLimb(GameObject limb)
    {
        Rigidbody limbRigidbody = limb.GetComponent<Rigidbody>();
        if (limbRigidbody != null)
        {
            // Determine the direction (forward or backward)
            Vector3 forceDirection = Random.Range(0, 2) == 0 ?  forceDirection = new Vector3(Random.Range(5, 10), 0, 0) : forceDirection = new Vector3(0, 0, Random.Range(5, 10));

            // Apply a force along the determined direction
            float forceMagnitude = Random.Range(2, 5);
            limbRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        }
    }

    private IEnumerator DestroyLimbs(GameObject head, GameObject leftArm, GameObject rightArm, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(head);
        Destroy(leftArm);
        Destroy(rightArm);
    }

    private IEnumerator DestroyParticleEffects(GameObject particleEffect, GameObject splashEffect, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(particleEffect);
        Destroy(splashEffect);
        
    }
}
