using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{

    public static TurretManager Instance;

    public GameObject turretPrefab;

    public Transform turretAttachmentPoint;

    // Keep track of the spawned turret
    private GameObject spawnedTurret;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTurret()
    {
        if (turretAttachmentPoint != null && turretPrefab != null)
        {
            // Instantiate the turretPrefab as a child of the attachment point
            spawnedTurret = Instantiate(turretPrefab, turretAttachmentPoint);

            // Set the local position and rotation of the turret relative to the attachment point
            spawnedTurret.transform.localPosition = Vector3.zero; // Adjust the local position as needed
            spawnedTurret.transform.localRotation = transform.rotation * Quaternion.Euler(-90, 0, 0); // Adjust the local rotation as needed
        }
    }
}
