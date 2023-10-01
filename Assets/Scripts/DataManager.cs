using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public string PlayerName;

    public string BestScorePlayerName = "Name";
    public int BestScore;

    public event Action OnSaveDataLoaded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadBestScore();
    }

    [Serializable]
    private class SaveData
    {
        public string name;
        public int score;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.name = BestScorePlayerName;
        data.score = BestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);

        Debug.Log("Best score was saved to " + Application.persistentDataPath + "/bestscore.json");
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/bestscore.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScorePlayerName = data.name;
            BestScore = data.score;

            OnSaveDataLoaded.Invoke();
        }
    }
}
