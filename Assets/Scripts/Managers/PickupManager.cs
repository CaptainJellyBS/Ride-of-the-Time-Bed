using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance { get; private set; }
    public GameObject doubleScorePickup, extraLifePickup, fireRatePickup, timeRestorePickup;
    public float dspChance, elpChance, frpChance, trpChance;
    public float pickupChance;
    public float scoreMultiplier = 1.0f;

    public float fireRateDuration = 5.0f, timeRestoreDuration = 5.0f, scoreBonusDuration = 5.0f;
    public float fireRateBonus, timeRestoreBonus;

    private void Awake()
    {
        if (Instance) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        elpChance += dspChance;
        frpChance += elpChance;
        trpChance += frpChance;
    }

    public void SpawnPickup(Vector3 position)
    {
        position = Vector3.Scale(new Vector3(1, 0, 1), position);
        float roll = Random.Range(0.0f, 1.0f);

        if(roll <= dspChance) { Instantiate(doubleScorePickup, position, Quaternion.identity); return; }
        if (roll > dspChance && roll <= elpChance) { Instantiate(extraLifePickup, position, Quaternion.identity); return; }
        if (roll > elpChance && roll <= frpChance) { Instantiate(fireRatePickup, position, Quaternion.identity); return; }
        if (roll > frpChance) { Instantiate(timeRestorePickup, position, Quaternion.identity); return; }
    }

    public void FireRatePickup()
    {
        StartCoroutine(FRPickupC());
    }
    IEnumerator FRPickupC()
    {
        PlayerMovement.Instance.fireRate += fireRateBonus;
        yield return new WaitForSeconds(fireRateDuration);
        PlayerMovement.Instance.fireRate -= fireRateBonus;
    }

    public void DoubleScorePickup()
    {
        StartCoroutine(DSPickupC());
    }

    IEnumerator DSPickupC()
    {
        scoreMultiplier = 2.0f;
        yield return new WaitForSeconds(scoreBonusDuration);
        scoreMultiplier = 1.0f;
    }

    public void TimeRestorePickup()
    {
        StartCoroutine(TRPickupC());
    }

    IEnumerator TRPickupC()
    {
        GameManager.Instance.timePointBonus += timeRestoreBonus;
        yield return new WaitForSeconds(timeRestoreDuration);
        GameManager.Instance.timePointBonus -= timeRestoreBonus;
    }
}
