using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public float aliveTime = 7.5f;
    Renderer[] renderers;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        StartCoroutine(Despawn());
    }

    protected IEnumerator Despawn()
    {
        yield return new WaitForSeconds(aliveTime - 2);

        for (int t = 0; t < 4; t++)
        {
            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }
            yield return new WaitForSeconds(0.3f);
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
            yield return new WaitForSeconds(0.3f);
        }

        for (int t = 0; t < 4; t++)
        {
            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }
            yield return new WaitForSeconds(0.1f);
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }

    public virtual void OnPickup()
    {
        Destroy(gameObject);
    }
}
