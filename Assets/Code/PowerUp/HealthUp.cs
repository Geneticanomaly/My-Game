using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip pickupSound;

    public PowerupEffect powerupEffect;

    public GameObject pickupEffect;

    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            Pickup(other);
        }
    }

    void Pickup(Collider player) 
    {
        Debug.Log("Health picked up!");
        Instantiate(pickupEffect, transform.position, transform.rotation);
        //audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(pickupSound);
        powerupEffect.Apply(player.gameObject);

        if (meshRenderer != null)
            meshRenderer.enabled = false;

        if (sphereCollider != null)
            sphereCollider.enabled = false;

        StartCoroutine(DestroyAfterDelay());
    }

        private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
