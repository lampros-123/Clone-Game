using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float paddingSide, paddingTop, paddingBot;

    [HideInInspector]
    public Transform TransformToFollow;

    new Camera camera;
    Transform topLeft, botRight;
    float desiredX, desiredY;
    float cameraWidth, cameraHeight;

    private void Start() {
        topLeft = GameObject.Find("TopLeft").transform;
        botRight = GameObject.Find("BotRight").transform;
        camera = GetComponent<Camera>();
        desiredX = transform.position.x;
        desiredY = transform.position.y;
        cameraHeight = 2f * camera.orthographicSize;
        cameraWidth = cameraHeight * camera.aspect;
    }

    void Update() {
        if (TransformToFollow == null) return;

        // set desired x coordinate
        if (TransformToFollow.position.x < transform.position.x) {
            float leftmostX = topLeft.position.x + cameraWidth / 2 - paddingSide;
            desiredX = Mathf.Max(leftmostX, TransformToFollow.position.x);
        } else {
            float rightmostX = botRight.position.x - cameraWidth / 2 + paddingSide;
            desiredX = Mathf.Min(rightmostX, TransformToFollow.position.x);
        }

        // set desired y coordinate
        if (TransformToFollow.position.y < transform.position.y) {
            float highestY = botRight.position.y + cameraHeight / 2 - paddingBot;
            desiredY = Mathf.Max(highestY, TransformToFollow.position.y);
        } else {
            float lowestY = topLeft.position.y - cameraHeight / 2 + paddingTop;
            desiredY = Mathf.Min(lowestY, TransformToFollow.position.y);
        }

        // handle situation where the whole screen fits on display
        if (topLeft.position.x >= transform.position.x - cameraWidth / 2 + paddingSide
            && botRight.position.x <= transform.position.x + cameraWidth / 2 - paddingSide) {
            desiredX = (topLeft.position.x + botRight.position.x) / 2;
        }
        if (botRight.position.y >= transform.position.y - cameraHeight / 2 + paddingBot
            && topLeft.position.y <= transform.position.y + cameraHeight / 2 - paddingTop) {
            desiredY = (topLeft.position.y + paddingTop + botRight.position.y - paddingBot) / 2;
        }

        MoveToDesiredPos();
    }

    void MoveToDesiredPos() {
        Vector3 desired = new Vector3(desiredX, desiredY, transform.position.z);
        float dist = Vector3.Distance(transform.position, desired);
        float speed = .05f;
        if (dist < .5f)
            speed = .6f - dist;
        transform.position = Vector3.Lerp(transform.position, desired, speed);
    }
}
