using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLogic : MonoBehaviour
{
    [SerializeField] float turretRange = 13f;
    [SerializeField] float turretRotationSpeed = 5f;

    private Transform playerTransform;
    private float fireRate;
    private float fireRateDelta;

    private EnemyAILogic currentTarget;

    private TurretGun currentGun;

    private AudioSource audioSource;
    public AudioClip turretShot;

    private void Start()
    {
        currentGun = GetComponentInChildren<TurretGun>();
        fireRate = currentGun.GetRateOfFire();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (currentTarget == null || !IsTargetInRange(currentTarget))
        {
            currentTarget = FindNewTarget();
        }

        if (currentTarget != null)
        {
            // Rotate the turret to face the current target
            RotateTurretTowardsTarget(currentTarget);

            fireRateDelta -= Time.deltaTime;
            if (fireRateDelta <= 0)
            {
                audioSource.PlayOneShot(turretShot);
                currentGun.Fire();
                fireRateDelta = fireRate;
            }
            
        }
    }

    private bool IsTargetInRange(EnemyAILogic target)
    {
        return Vector3.Distance(transform.position, target.transform.position) <= turretRange;
    }

    private void RotateTurretTowardsTarget(EnemyAILogic target)
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        targetRotation.eulerAngles = new Vector3(-90, targetRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turretRotationSpeed * Time.deltaTime);
    }

    private EnemyAILogic FindNewTarget()
    {
        // Find all active EnemyAILogic objects in the scene
        EnemyAILogic[] enemyAIs = FindObjectsOfType<EnemyAILogic>();
        
        // Filter out destroyed targets and those out of range
        List<EnemyAILogic> validTargets = new List<EnemyAILogic>();

        foreach (EnemyAILogic enemyAI in enemyAIs)
        {
            if (enemyAI != null && IsTargetInRange(enemyAI))
            {
                validTargets.Add(enemyAI);
            }
        }

        if (validTargets.Count > 0)
        {
            return validTargets[Random.Range(0, validTargets.Count)];
        }

        return null;
    }

    public Transform GetCurrentTargetTransform()
    {
        if (currentTarget != null)
        {
            return currentTarget.transform;
        }
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
}
