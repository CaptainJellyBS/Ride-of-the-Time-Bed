using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { damage, timeDrain};
public abstract class Enemy : MonoBehaviour
{
    public int hp, points;
    public float damage;
    public DamageType damageType;
    bool hasBeenInScreen;
    public bool spawnsPickup = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        hasBeenInScreen = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!hasBeenInScreen)
        {
            if (!(transform.position.x >= 25.0f || transform.position.x < -25.0f || transform.position.z >= 15.0f || transform.position.z <= -15.0f))
            {
                hasBeenInScreen = true;
            }
        }
        else
        {
            if (transform.position.x >= 25.0f || transform.position.x < -25.0f || transform.position.z >= 15.0f || transform.position.z <= -15.0f)
            {
                Destroy(gameObject);
            }
        }

        if (hp<=0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        GameManager.Instance.Score += (int)(points * PickupManager.Instance.scoreMultiplier);
        if (spawnsPickup)
        {
            GameManager.Instance.CameraShake(0.2f, 0.075f);

            if (Random.Range(0.000f, 1.000f) <= PickupManager.Instance.pickupChance)
            {
                PickupManager.Instance.SpawnPickup(transform.position);
            }
        }
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Destroy(collision.gameObject);
            if (collision.GetContact(0).thisCollider.CompareTag("IndestructibleEnemy")) { return; }
            hp -= 1;
            GameManager.Instance.PlayEnemyHitSound();
            StartCoroutine(DamageFlash(collision.GetContact(0).thisCollider.gameObject));
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            switch (damageType)
            {
                case DamageType.damage: PlayerMovement.Instance.TakeDamage(1); break;
                case DamageType.timeDrain: PlayerMovement.Instance.TakeTimeDamage(damage); break;
            }
            Destroy(gameObject);
        }
    }

    IEnumerator DamageFlash(GameObject o)
    {
        Renderer renderer = o.GetComponent<Renderer>();
        renderer.material.EnableKeyword("_EMISSION");
        renderer.material.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.1f);
        renderer.material.SetColor("_EmissionColor", Color.black);
    }
}
