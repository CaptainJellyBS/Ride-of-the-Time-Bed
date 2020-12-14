using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableProjectile : Enemy
{
    public float speed;
    protected override void Update()
    {
        transform.position += transform.forward * speed * GameManager.Instance.GetCurrentTimeSpeed() * Time.deltaTime;
        base.Update();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if(collision.GetContact(0).otherCollider.CompareTag("IndestructibleEnemy"))
        {
            Destroy(gameObject);
        }
        base.OnCollisionEnter(collision);
    }
}
