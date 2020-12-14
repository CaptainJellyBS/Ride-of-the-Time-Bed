using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float moveSpeed;

    public void Init(float speed)
    {
        moveSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * GameManager.Instance.GetCurrentTimeSpeed() * Time.deltaTime;

        if (transform.position.x >= 25.0f || transform.position.x<-25.0f || transform.position.z >= 15.0f || transform.position.z <= -15.0f)
        {
            Destroy(gameObject);
        }
    }
}
