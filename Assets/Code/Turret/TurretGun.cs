using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfFire = 1f;

    public float GetRateOfFire()
    {
        return rateOfFire;
    }

    public void Fire()
    {
        Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(90, 0, 0));
    }
}
