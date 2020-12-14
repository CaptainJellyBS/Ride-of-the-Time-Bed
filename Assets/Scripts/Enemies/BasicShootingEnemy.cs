using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShootingEnemy : Enemy
{
    bool isWithinFireRange, isWithinStopRange, canShoot;
    public float speed, fireRate, fireRange, stopRange;
    public GameObject projectile;
    public Transform projectileOrigin;

    // Start is called before the first frame update
    protected override void Start()
    {
        isWithinFireRange = false; canShoot = true;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        isWithinFireRange = Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position) < fireRange;
        isWithinStopRange = Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position) < stopRange;
        transform.LookAt(Vector3.Scale(PlayerMovement.Instance.transform.position, new Vector3(1, 0, 1)), Vector3.up);

        if (isWithinFireRange) { Shoot(); }
        if(!isWithinStopRange)  { Move(); }

        base.Update();
    }

    void Shoot()
    {
        if(!canShoot || GameManager.Instance.GetCurrentTimeSpeed() <= 0.0f) { return; }
        Instantiate(projectile, projectileOrigin.position, transform.rotation);
        canShoot = false;
        StartCoroutine(ShootC());
    }

    IEnumerator ShootC()
    { 
        yield return new WaitForSeconds(1 / (fireRate * GameManager.Instance.GetCurrentTimeSpeed()));
        canShoot = true;
    }

    void Move()
    {
        transform.position += transform.forward * speed * GameManager.Instance.GetCurrentTimeSpeed() * Time.deltaTime;
        base.Update();
    }
}
