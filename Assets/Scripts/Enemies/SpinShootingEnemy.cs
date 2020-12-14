using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinShootingEnemy : Enemy
{
    public float flySpeed, rotateSpeed, fireRate;
    public GameObject projectile;
    public Transform spinner, projectileOrigin;
    public bool turnToPlayerOnSpawn = true;

    bool canShoot;

    protected override void Start()
    {
        canShoot = projectile;
        base.Start();
        if(Random.Range(0,2) == 0) { rotateSpeed *= -1; }
        if (turnToPlayerOnSpawn) { transform.LookAt(PlayerMovement.Instance.transform, Vector3.up); }
    }
    protected override void Update()
    {
        transform.position += transform.forward * flySpeed * GameManager.Instance.GetCurrentTimeSpeed() * Time.deltaTime;
        spinner.Rotate(Vector3.up, rotateSpeed * GameManager.Instance.GetCurrentTimeSpeed() * Time.deltaTime);

        Shoot();
        base.Update();
    }
    void Shoot()
    {
        if (!canShoot || GameManager.Instance.GetCurrentTimeSpeed() <= 0.0f) { return; }
        Instantiate(projectile, projectileOrigin.position, spinner.transform.rotation);
        canShoot = false;
        StartCoroutine(ShootC());
    }

    IEnumerator ShootC()
    {
        yield return new WaitForSeconds(1 / (fireRate * GameManager.Instance.GetCurrentTimeSpeed()));
        canShoot = true;
    }
}
