using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speedometer : MonoBehaviour
{
    private Transform needleTransform;

    private const float maxSpeedAngle = 0;
    private const float zeroSpeedAngle = 180;

    private float speed;
    private float speedMax;

    public Rigidbody sphereRB;

    private void Awake()
    {
        needleTransform = transform.Find("Needle");

        speed = 0f;
        speedMax = 140f;
    }

    void Update()
    {
        speed = sphereRB.velocity.magnitude;

        speed = Mathf.Clamp(speed, 0, speedMax);

        float rotationAngle = GetSpeedRotation(speed * 2.5f);

        needleTransform.eulerAngles = new Vector3(0, 0, rotationAngle);
    }

    private float GetSpeedRotation(float currentSpeed)
    {
        float totalAngleSize = zeroSpeedAngle - maxSpeedAngle;

        float speedNormalized = currentSpeed / speedMax;

        return zeroSpeedAngle - speedNormalized * totalAngleSize;
    }
}