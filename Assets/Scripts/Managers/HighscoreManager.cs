using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance { get; private set; }
    HighscoreData data;
    public string playerName;
    public bool tutorialEnabled, alternateControls;  

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null) { Destroy(this); return; }
        Instance = this;

        DontDestroyOnLoad(this);
        data = new HighscoreData(10);

        LoadHighscores();
        Debug.Log(data.ToString());
    }

    public void AddScore(int score)
    {
        data.AddScore(playerName, score);
    }

    public void AddScore(string name, int score)
    {
        data.AddScore(name, score);
    }


    //Saving data code stolen from: https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity#toc-anchor-001
    public void SaveHighscores()
    {
        SaveData saveData = new SaveData();
        saveData.names = data.Names;
        saveData.scores = data.Scores;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, saveData);
        file.Close();
    }

    //Loading data code stolen from: https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity#toc-anchor-001
    public void LoadHighscores()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();
            data.Reset();
            for (int i = 0; i < save.names.Length; i++)
            {
                data.Names[i] = save.names[i];
                data.Scores[i] = save.scores[i];
            }
        }

        else
        {
            data.Reset();
            Debug.Log("No game saved!");
            SaveHighscores();
        }
    }

    public void ClearSave()
    {
        data.Reset();
        SaveHighscores();
    }

    public string HighScoreString()
    {
        return data.ToString();
    }
}
