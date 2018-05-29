using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICloneController {

    List<Action> GetActions();
    int GetCurrentFrame();
    void SetRunning(bool running);
}
