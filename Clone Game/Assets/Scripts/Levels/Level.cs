using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level {
    public static int MaskStar1 = 1;
    public static int MaskStar2 = 1 << 1;
    public static int MaskStar3 = 1 << 2;

    private static int[] RequiredStars = {
        0, 1, 3,
        6, 9, 13
    };

    public int ID { get; set; }
    public string LevelName { get; set; }
    public string SceneName { get; set; }
    public bool Completed { get; set; }
    public int Stars { get; set; }
    public bool Locked { get; set; }

    public Level(int id, string levelname) {
        this.ID = id;
        this.LevelName = levelname;
        SceneName = "Level" + levelname;
        Completed = false;
        Stars = 0;
        Locked = true;
    }

    public Level(int id, string levelname, string sceneName, bool completed, int stars, bool locked) {
        this.ID = id;
        this.LevelName = levelname;
        this.SceneName = sceneName;
        this.Completed = completed;
        this.Stars = stars;
        this.Locked = locked;
    }

    public void Complete() {
        Completed = true;
    }

    public void Complete(int stars) {
        this.Stars |= stars;
        Completed = true;
    }

    public void Lock() {
        Locked = true;
    }
    public void Unlock() {
        Locked = false;
    }

    public int GetRequiredStars() {
        if(RequiredStars.Length > ID) {
            return RequiredStars[ID];
        }
        Debug.Log("Required stars not set");
        return -1;
    }
}
