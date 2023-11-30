using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/DoublePointsBuff")]
public class DoublePointsBuff : PowerupEffect
{

    public AudioClip soundEffect;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerStats>().EnableDoublePoints();
    }
}
