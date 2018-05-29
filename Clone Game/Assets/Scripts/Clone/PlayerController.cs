using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICloneController {
    List<Action> actions = new List<Action>();
    int currentFrame = 0;

    Rigidbody2D rb;
    bool keyRight = false;
    bool keyLeft = false;
    bool keyUp = false;

    bool running = false;

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
        if (!running || PauseManager.Paused) return;

        currentFrame++;
        if (Input.GetKey(KeyCode.RightArrow) != keyRight)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                actions.Add(new Action(currentFrame, Action.KEYDOWN_RIGHT));
            else actions.Add(new Action(currentFrame, Action.KEYUP_RIGHT));
            keyRight = Input.GetKey(KeyCode.RightArrow);
        }
        if (Input.GetKey(KeyCode.LeftArrow) != keyLeft)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                actions.Add(new Action(currentFrame, Action.KEYDOWN_LEFT));
            else actions.Add(new Action(currentFrame, Action.KEYUP_LEFT));
            keyLeft = Input.GetKey(KeyCode.LeftArrow);
        }
        if (Input.GetKey(KeyCode.UpArrow) && !keyUp)
        {
            actions.Add(new Action(currentFrame, Action.KEYDOWN_UP));
            keyUp = true;
        }
        if(keyUp && !Input.GetKey(KeyCode.UpArrow)) { 
            keyUp = false;
        }
    }

    private void OnDestroy()
    {
        actions.Add(new Action(currentFrame, Action.KEYUP_LEFT));
        actions.Add(new Action(currentFrame, Action.KEYUP_RIGHT));
        actions.Add(new Action(currentFrame, Action.KEYUP_UP));
    }

    public List<Action> GetActions()
    {
        return actions;
    }

    public int GetCurrentFrame()
    {
        return currentFrame;
    }

    public void SaveActions(string fileName)
    {
        string filePath = Path.Combine(Application.dataPath, fileName);
        ControllingData data = new ControllingData();
        data.actions = actions;
        string jsonData = JsonUtility.ToJson(data);
        if(!File.Exists(filePath))
            File.Create(filePath).Dispose();
        File.WriteAllText(filePath, jsonData);

        if (File.Exists(filePath))
        {
            ControllingData loadedData = JsonUtility.FromJson<ControllingData>(jsonData);
            actions = loadedData.actions;
        }
        else
        {
            Debug.Log("ERROR: File " + filePath + " not found (loading Clonecontroldata)");
        }
    }
}
