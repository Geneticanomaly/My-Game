using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffect : MonoBehaviour
{

    /* public GameObject groundEffect; */

    void OnTriggerEnter(Collider other) 
    {

        if (other.CompareTag("Player"))
        {
            DestroyGroundEffect(other);
        }
    }

    void DestroyGroundEffect(Collider player)
    {
        Debug.Log("Ground effect touched!");
        Destroy(gameObject);

    }
}
