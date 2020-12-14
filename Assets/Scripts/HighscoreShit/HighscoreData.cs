using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HighscoreData
{
    public string[] Names { get; private set; }
    public int[] Scores { get; private set; }
    int l;

    public HighscoreData(int length)
    {
        l = length;
        Names = new string[length];
        Scores = new int[length];
    }
    /// <summary>
    /// Add a score to the highscore list, which ensures the list stays ordered.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    public void AddScore(string name, int score)
    {
        if(name == string.Empty || name == null || name == "" || name == " ")
        {
            name = "Unnamed Dweeb";
        }

        for (int i = l; i > 0; i--)
        {
            if (Scores[i - 1] <= score)
            {
                if (i >= l) { continue; } //The lowest score will be overwritten by the next loop pass, ignore it to avoid OutOfBounds issues
                Scores[i] = Scores[i - 1];
                Names[i] = Names[i - 1];
            }
            else
            {
                if (i >= l) { return; } //Return if our score isn't high enough to go into the highscore list.
                Scores[i] = score;
                Names[i] = name;
                return;
            }
        }
        //If we arrived here, that means we set the highest score.
        Scores[0] = score;
        Names[0] = name;
    }

    public override string ToString()
    {
        string result = string.Empty;

        for (int i = 0; i < l - 1; i++)
        {
            result += ParseEntry(i);
            result += "\n";
        }
        result += ParseEntry(l - 1);

        return result;
    }

    string ParseEntry(int i)
    {
        string result = Names[i];

        result = result.PadRight(22, ' ');
        result += " : " + Scores[i];
        return result;
    }

    public void Reset()
    {
        Names[9] = "Empty"; Scores[9] = 0;
        Names[8] = "Empty"; Scores[8] = 0;
        Names[7] = "Empty"; Scores[7] = 0;
        Names[6] = "Empty"; Scores[6] = 0;
        Names[5] = "Empty"; Scores[5] = 0;
        Names[4] = "Empty"; Scores[4] = 0;
        Names[3] = "Empty"; Scores[3] = 0;
        Names[2] = "Empty"; Scores[2] = 0;
        Names[1] = "Empty"; Scores[1] = 0;
        Names[0] = "Empty"; Scores[0] = 0;
    }
}

