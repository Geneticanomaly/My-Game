using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlade : MonoBehaviour
{
    public float rotationSpeed = 100.0f; 

    void Update()
    {
         transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

    }
}
