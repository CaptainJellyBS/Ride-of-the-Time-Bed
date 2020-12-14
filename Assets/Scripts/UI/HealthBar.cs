using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    float hpValue = 1.0f;
    public bool flickering;
    public float flickerSpeed;

    private void Start()
    {
        flickering = false;
        StartCoroutine(Flicker());
    }

    [SerializeField]
    public float HPValue
    {
        get { return hpValue; }
        set
        {
            hpValue = Mathf.Clamp(value, 0.0f, 1.0f);
            bar.transform.localScale = new Vector3(hpValue, 1);
            flickering = hpValue < 0.2f;
        }
    }

    [SerializeField]
    public float ConcreteHPValue
    {
        get { return HPValue * maxConcreteHPValue; }
        set { HPValue = value / maxConcreteHPValue; }
    }
    public float maxConcreteHPValue;

    IEnumerator Flicker()
    {
        while (true)
        {
            if (flickering)
            {
                bar.enabled = false;
                yield return new WaitForSeconds(1 / flickerSpeed);
                bar.enabled = true;
                yield return new WaitForSeconds(1 / flickerSpeed);
            }

            yield return null;
        }
    }

    public void FlickerForTime(float time)
    {
        StartCoroutine(FlickerForTimeC(time));
    }

    IEnumerator FlickerForTimeC(float time)
    {
        flickering = true;
        yield return new WaitForSeconds(time);
        flickering = false;
    }
}
