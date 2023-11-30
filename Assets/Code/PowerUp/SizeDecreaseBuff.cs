using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SizeDecrease")]
public class SizeDecreaseBuff : PowerupEffect
{
    public override void Apply(GameObject target)
    {
        GameObject carObject = GameObject.FindGameObjectWithTag("Car");
        if (carObject != null)
        {
            CarController carController = carObject.GetComponent<CarController>();
            carController.EnableDecreaseSizeBuff();
        }
    }
}
