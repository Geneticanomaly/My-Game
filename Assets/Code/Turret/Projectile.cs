using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float destroyDelay = 2f; // Adjust this value to change the delay

    private float lifeTimer;
    private bool isDestroyed = false;

    void Start()
    {
        lifeTimer = 0f;
    }

    void Update()
    {
        // Check if the projectile has been destroyed
        if (isDestroyed)
        {
            return;
        }

        // Move the projectile
        transform.Translate(new Vector3(0f, projectileSpeed * Time.deltaTime, 0f));

        // Update the timer
        lifeTimer += Time.deltaTime;

        // Check if the timer has exceeded the destroy delay
        if (lifeTimer >= destroyDelay)
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        isDestroyed = true; // Set the flag to mark the projectile as destroyed
        // Destroy the projectile
        Destroy(gameObject);
    }
}