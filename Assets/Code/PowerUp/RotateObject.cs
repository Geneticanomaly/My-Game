using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public float rotationSpeed;

    void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    void Update()
    {
        
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }
}
