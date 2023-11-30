using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    /* public int healAmount = 20; */
    private AudioSource audioSource;

    public AudioClip[] pickupSound;

    public List<PowerupEffect> powerupEffects;

    public GameObject pickupEffect;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    void Start()
    {
        // Get references to the colliders
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
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
        Debug.Log("Power up picked up!");

        // Spawn an effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        // Randomly select a power-up effect
        if (powerupEffects.Count > 0)
        {
            int randomIndex = Random.Range(0, powerupEffects.Count);
            PowerupEffect randomPowerupEffect = powerupEffects[randomIndex];

            audioSource = GetComponent<AudioSource>();
            audioSource.clip = pickupSound[randomIndex];
            audioSource.PlayOneShot(audioSource.clip);
            // Apply the randomly selected effect to the player
            randomPowerupEffect.Apply(player.gameObject);
        }

        if (meshRenderer != null)
            meshRenderer.enabled = false;

        if (boxCollider != null)
            boxCollider.enabled = false;

        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
