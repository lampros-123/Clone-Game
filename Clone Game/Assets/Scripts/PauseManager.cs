using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool Paused { get; set; }

    public GameObject pauseOverlay;
    float prevTimeScale;

    void Start() {
        pauseOverlay.SetActive(false);
        SceneManager.sceneLoaded += LevelFinishedLoading;
    }

    public void LevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        Paused = false;
    }

    public void Pause() {
        Paused = true;
        pauseOverlay.SetActive(false);
        pauseOverlay.SetActive(true);
    }

    public void Unpause() {
        Paused = false;
        pauseOverlay.GetComponent<Animator>().SetTrigger("FadeOut");
        Invoke("DeactivateOverlay", 2f);
    }

    public void DeactivateOverlay() {
        if(!Paused)
            pauseOverlay.SetActive(false);
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (Paused) Unpause();
            else Pause();
        }
    }
}
