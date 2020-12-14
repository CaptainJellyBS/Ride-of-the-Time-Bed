using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour
{
    public Text text;

    public void SetHighscores()
    {
        text.text = HighscoreManager.Instance.HighScoreString();
    }
}
