using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{

    public static TurretManager Instance;

    public GameObject turretPrefab;

    public Transform turretAttachmentPoint;

    private GameObject spawnedTurret;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnTurret()
    {
        if (turretAttachmentPoint != null && turretPrefab != null)
        {
            spawnedTurret = Instantiate(turretPrefab, turretAttachmentPoint);

            spawnedTurret.transform.localPosition = Vector3.zero; 
            spawnedTurret.transform.localRotation = transform.rotation * Quaternion.Euler(-90, 0, 0); 
        }
    }
}
