using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRestorePickup : Pickup
{
    public override void OnPickup()
    {
        PickupManager.Instance.TimeRestorePickup();
        base.OnPickup();
    }
}
