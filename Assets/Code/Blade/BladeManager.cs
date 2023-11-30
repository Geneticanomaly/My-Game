using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeManager : MonoBehaviour
{

    public static BladeManager Instance;

    public GameObject bladePrefab;

    public Transform leftBladeAttachmentPoint;
    public Transform rightBladeAttachmentPoint;

    private GameObject spawnedBladeLeft;
    private GameObject spawnedBladeRight;

    private float yOffset = -1.7f;

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

    public void SpawnBlades()
    {
        if (leftBladeAttachmentPoint != null && bladePrefab != null)
        {
            spawnedBladeLeft = Instantiate(bladePrefab, leftBladeAttachmentPoint);
            spawnedBladeLeft.transform.localPosition = new Vector3(-0.4f, yOffset, 0f);
            spawnedBladeLeft.transform.localRotation = transform.rotation * Quaternion.Euler(0, -90, 0);
        }

        if (rightBladeAttachmentPoint != null && bladePrefab != null)
        {
            spawnedBladeRight = Instantiate(bladePrefab, rightBladeAttachmentPoint);
            spawnedBladeRight.transform.localPosition = new Vector3(0.4f, yOffset, 0f);
            spawnedBladeRight.transform.localRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
        }
    }
}
