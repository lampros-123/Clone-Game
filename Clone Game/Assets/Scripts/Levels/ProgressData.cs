using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData
{
    private static ProgressData instance;

    private string fileName = "ckUREIdU.csv";
    private int numberOfLevels = 3;

    private List<Level> levels;
    private int totalStars = 0;

    public static ProgressData GetInstance() {
        if (instance == null) instance = new ProgressData();
        return instance;
    }

    private ProgressData() {
        LoadData();
    }

    private void InitLevels() {
        for (int i = 0; i < levels.Count; i++) {
            if (levels[i].GetRequiredStars() <= totalStars) {
                levels[i].Unlock();
            }
        }
    }

    private void LoadData() {
        try {
            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                BinaryFormatter formatter = new BinaryFormatter();
                levels = (List<Level>)formatter.Deserialize(stream);
                for (int i = 0; i < levels.Count; i++) {
                    totalStars += levels[i].Stars;
                }
            }
        } catch (System.Exception e) {
            Debug.Log("Exception in loading progress data: " + e.Message);
            levels = new List<Level>();
            while (levels.Count < numberOfLevels) {
                levels.Add(new Level(levels.Count, string.Format("{0:00}", levels.Count + 1)));
            }
            totalStars = 0;
            SaveProgress();
        }
        InitLevels();
    }

    public void SaveProgress() {
        try {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
                formatter.Serialize(stream, levels);
            }
            InitLevels();
        } catch (System.Exception e) {
            Debug.Log("Error saving progressdata: " + e.Message);
        }
    }

    public Level GetLevel(int id) {
        foreach (Level level in levels) {
            if (level.ID == id) return level;
        }
        return null;
    }

    public Level GetLevelBySceneName(string sceneName) {
        foreach (Level level in levels) {
            if (level.SceneName == sceneName) return level;
        }
        return null;
    }

    public Level GetLevelByLevelName(string levelName) {
        foreach (Level level in levels) {
            if (level.LevelName == levelName) return level;
        }
        return null;
    }

    public int getNumberOfLevels() {
        return numberOfLevels;
    }
}
