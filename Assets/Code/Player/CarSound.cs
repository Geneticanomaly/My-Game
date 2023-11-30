using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{

    public Rigidbody sphereRB;
    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.pitch = 0.5f + Mathf.Lerp(audioSource.pitch -0.5f, sphereRB.velocity.magnitude / 30f, Time.deltaTime * 5f);
    }
}
