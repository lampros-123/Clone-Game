using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredPlatform : MonoBehaviour, ITriggerable {
    public Transform startPos;
    public Transform endPos;
    public Transform platform;
    public float executionTime;

    float cycleTime = 0;
    float cycleFactor = 0;

    void Start() {
        platform.position = startPos.position;
    }

    public void Activated()
    {
        cycleFactor = 1;
    }

    public void Deactivated()
    {
        cycleFactor = -1;
    }

    void FixedUpdate () {
        if(PauseManager.Paused) {
            return;
        }

        if(cycleTime > executionTime)
        {
            cycleTime = executionTime;
            cycleFactor = 0;
        }
        if(cycleTime < 0)
        {
            cycleTime = 0;
            cycleFactor = 0;
        }

        cycleTime += Time.deltaTime * cycleFactor;
        float radians = cycleTime / executionTime * Mathf.PI;
        float positionFrac = (Mathf.Cos(radians) - 1.0f) / -2.0f;
        platform.position = Vector3.Lerp(startPos.position, endPos.position, positionFrac);
    }
}
