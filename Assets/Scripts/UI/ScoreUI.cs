using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreValueText;
    private Vector3 origScale;
    public Vector3 scaleFactor = new Vector3(1, 1, 1);
    public float inflateTime;
    RectTransform rt;


    private int scoreValue;
    public int ScoreValue
    {
        get { return scoreValue; }
        set 
        {
            scoreValue = value;
            scoreValueText.text = scoreValue.ToString();
            if (gameObject.activeInHierarchy) { StartCoroutine(ScoreInflate()); }
        }
    }

    private void Start()
    {
        origScale = transform.localScale;
        rt = GetComponent<RectTransform>();
    }

    IEnumerator ScoreInflate()
    {
        transform.localScale = Vector3.Scale(transform.localScale, scaleFactor);
        yield return new WaitForSeconds(inflateTime);
        transform.localScale = origScale;
    }
}
