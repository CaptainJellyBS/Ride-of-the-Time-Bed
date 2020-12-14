using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifePickup : Pickup
{
    public override void OnPickup()
    {
        PlayerMovement.Instance.Lives++;
        base.OnPickup();
    }
}
