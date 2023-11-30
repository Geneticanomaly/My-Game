using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public void DestroyPowerups()
    {
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");

        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup);
        }

        GameObject[] groundEffects = GameObject.FindGameObjectsWithTag("GroundEffect");

        foreach (GameObject groundEffect in groundEffects)
        {
            Destroy(groundEffect);
        }

        GameObject[] flyingEffects = GameObject.FindGameObjectsWithTag("FlyingEffect");

        foreach (GameObject flyingEffect in flyingEffects)
        {
            Destroy(flyingEffect);
        }
    }
}
