using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text highscores;
    public Toggle tutorialToggle, altControlToggle;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            HighscoreManager.Instance.tutorialEnabled = PlayerPrefs.GetInt("Tutorial") == 1;
        }
        else
        {
            HighscoreManager.Instance.tutorialEnabled = true;
        }
        if (PlayerPrefs.HasKey("AltControl"))
        {
            HighscoreManager.Instance.alternateControls = PlayerPrefs.GetInt("AltControl") == 1;
        }
        else
        {
            HighscoreManager.Instance.alternateControls = false;
        }
        tutorialToggle.isOn = HighscoreManager.Instance.tutorialEnabled;
        altControlToggle.isOn = HighscoreManager.Instance.alternateControls;
        UpdateHighscoreText();
    }

    public void UpdateHighscoreText()
    {
        highscores.text = HighscoreManager.Instance.HighScoreString();
    }

    public void EnableTutorial(bool well)
    {
        HighscoreManager.Instance.tutorialEnabled = well;
        if (well)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Tutorial", 0);

        }
    }

    public void EnableAlternativeControls(bool well)
    {
        HighscoreManager.Instance.alternateControls = well;
        if (well)
        {
            PlayerPrefs.SetInt("AltControl", 1);
        }
        else
        {
            PlayerPrefs.SetInt("AltControl", 0);

        }
    }
    public void ChangePlayerName(string name)
    {
        HighscoreManager.Instance.playerName = name;
    }
}
