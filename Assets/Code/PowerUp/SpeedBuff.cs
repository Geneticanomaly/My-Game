using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedBuff : PowerupEffect
{
   public float amount = 50;

   public override void Apply(GameObject target)
   {
        GameObject carObject = GameObject.FindGameObjectWithTag("Car");
        if (carObject != null)
        {
            CarController carController = carObject.GetComponent<CarController>();
            carController.EnableSpeedBuff(amount);
        }

        /* target.GetComponent<PlayerStats>().speed += amount; */
   }
}
