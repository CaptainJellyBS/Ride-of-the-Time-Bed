using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public UnityEvent OnStart;

    private void Start()
    {
        if (HighscoreManager.Instance.tutorialEnabled)
        {
            OnStart.Invoke();
        }
        HighscoreManager.Instance.tutorialEnabled = false;
        PlayerPrefs.SetInt("Tutorial", 0);
    }
}
