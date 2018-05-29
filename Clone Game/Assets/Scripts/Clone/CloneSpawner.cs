using System.Collections;
using UnityEngine;

public class CloneSpawner : MonoBehaviour
{
    public GameObject obj;
    public float animationDelay = .6f;
    public float secondDelay = 5;
    public float initialDelay = 0;
    public bool destroyOnCollision = false;

    GameObject spawnPoint;
    Animator cloneMachineAnim;
    ICloneController controller;
    CameraController camController;

    bool waitForFramesDone = false;
    long currentFrame = 0;

    private void Start() {
        spawnPoint = GameObject.Find("SpawnPoint");
        cloneMachineAnim = GameObject.Find("CloneMachine").GetComponent<Animator>();
        controller = GetComponent<ICloneController>();
        camController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        StartCoroutine(Spawner());
    }

    IEnumerator WaitForFramesInSeconds(float seconds) {
        waitForFramesDone = false;
        long startFrame = currentFrame;
        float frameDuration = seconds / Time.fixedDeltaTime;

        while (currentFrame < startFrame + frameDuration)
            yield return null;
        waitForFramesDone = true;
    }

    IEnumerator Spawner() {
        do {
            yield return WaitForFramesInSeconds(initialDelay - animationDelay);
        } while (!waitForFramesDone);

        while (true) {
            cloneMachineAnim.SetTrigger("Spawn");
            do {
                yield return WaitForFramesInSeconds(animationDelay);
            } while (!waitForFramesDone);


            controller.SetRunning(true);

            GameObject clone = Instantiate(obj);
            clone.transform.position = spawnPoint.transform.position;
            CloneController cloneController = clone.GetComponent<CloneController>();
            cloneController.frameOffset = controller.GetCurrentFrame();
            cloneController.destroyOnCollision = destroyOnCollision;
            cloneController.SetController(controller);

            if (camController != null && camController.TransformToFollow == null)
                camController.TransformToFollow = clone.transform;

            do {
                yield return WaitForFramesInSeconds(secondDelay - animationDelay);
            } while (!waitForFramesDone);
        }
    }

    private void FixedUpdate() {
        if(!PauseManager.Paused) {
            currentFrame++;
        }
    }
}
