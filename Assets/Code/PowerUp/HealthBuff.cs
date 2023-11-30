using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/HealthBuff")]
public class HealthBuff : PowerupEffect
{

    public int amount = 1;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerStats>().Heal(amount);
    }
}
