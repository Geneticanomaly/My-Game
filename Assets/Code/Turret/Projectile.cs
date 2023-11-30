using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float destroyDelay = 2f;

    private float lifeTimer;
    private bool isDestroyed = false;

    void Start()
    {
        lifeTimer = 0f;
    }

    void Update()
    {
        if (isDestroyed)
        {
            return;
        }

        // Move the projectile
        transform.Translate(new Vector3(0f, projectileSpeed * Time.deltaTime, 0f));

        lifeTimer += Time.deltaTime;

        if (lifeTimer >= destroyDelay)
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        isDestroyed = true;
        // Destroy the projectile
        Destroy(gameObject);
    }
}