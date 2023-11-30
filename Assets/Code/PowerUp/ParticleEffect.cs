using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {

        if (other.CompareTag("Player"))
        {
            DestroyParticleEffect(other);
        }
    }

    void DestroyParticleEffect(Collider player)
    {
        Destroy(gameObject);

    }
}
