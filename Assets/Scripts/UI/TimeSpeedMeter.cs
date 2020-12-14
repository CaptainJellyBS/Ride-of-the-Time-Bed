using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSpeedMeter : MonoBehaviour
{
    public Image indicator;
    public float IndicatorSetting
    {
        set
        {
            indicator.rectTransform.rotation = Quaternion.AngleAxis(value * -30 + 90, Vector3.forward);
        }
    }
}
