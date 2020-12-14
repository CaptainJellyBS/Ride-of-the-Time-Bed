using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePickup : Pickup
{
    public override void OnPickup()
    {
        PickupManager.Instance.FireRatePickup();
        base.OnPickup();
    }
}
