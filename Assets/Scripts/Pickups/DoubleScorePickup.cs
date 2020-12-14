using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScorePickup : Pickup
{
    public override void OnPickup()
    {
        PickupManager.Instance.DoubleScorePickup();
        base.OnPickup();
    }
}
