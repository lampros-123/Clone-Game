using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PreprogrammedController : MonoBehaviour, ICloneController {
    public string actionSourceFileName;

    List<Action> actions = new List<Action>();
    int currentFrame = 0;

    bool running = false;

    public void LoadActionData()
    {
        string filePath = Path.Combine(Application.dataPath, actionSourceFileName);
        if(File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ControllingData loadedData = JsonUtility.FromJson<ControllingData>(jsonData);
            actions = loadedData.actions;
        }
        else
        {
            Debug.Log("ERROR: File " + filePath + " not found (loading Clonecontroldata)");
        }
    }

    public void Awake()
    {
        LoadActionData();
        SetRunning(true);
    }


    public void SetRunning(bool running)
    {
        this.running = running;
    }

    public bool IsRunning()
    {
        return running;
    }

    private void FixedUpdate()
    {
        running = !PauseManager.Paused;
        if (!running) return;

        currentFrame++;
    }

    public List<Action> GetActions()
    {
        return actions;
    }

    public int GetCurrentFrame()
    {
        return currentFrame;
    }
}
