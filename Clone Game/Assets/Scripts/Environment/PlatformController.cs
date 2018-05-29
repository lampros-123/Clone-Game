using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public Transform startPos;
    public Transform endPos;
    public Transform platform;
    public float period;

    float timeInPeriod = 0;

	void Start () {
        platform.position = startPos.position;
	}
	
	void FixedUpdate () {
        if (PauseManager.Paused) {
            return;
        }

        timeInPeriod = (timeInPeriod + Time.deltaTime) % period;
        float radians = timeInPeriod / period * 2.0f*Mathf.PI;
        float positionFrac = (Mathf.Cos(radians) - 1.0f) / -2.0f;
        platform.position = Vector3.Lerp(startPos.position, endPos.position, positionFrac);
	}
}
