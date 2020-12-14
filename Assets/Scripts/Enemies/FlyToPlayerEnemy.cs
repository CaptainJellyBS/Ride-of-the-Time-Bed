using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToPlayerEnemy : Enemy
{
    public float speed;

    // Update is called once per frame
    protected override void Update()
    {
        transform.LookAt(Vector3.Scale(PlayerMovement.Instance.transform.position, new Vector3(1, 0, 1)), Vector3.up);
        transform.position += transform.forward * speed * GameManager.Instance.GetCurrentTimeSpeed() * Time.deltaTime;
        base.Update();
    }
}
