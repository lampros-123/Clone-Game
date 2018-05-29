using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance;

    Animator animator;

    private string levelToLoad;

    public void OnSceneWasLoaded(Scene scene, LoadSceneMode mode) {
        animator = GameObject.Find("FadeToBlack").GetComponent<Animator>();
    }

    private void OnEnable() {
        if(instance == null) {
            instance = this;
        }
        if (instance != this) {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
        if(instance == this) {
            instance = null;
        }
    }

    public void Load(string scene) {
        animator.SetTrigger("FadeOut");
        levelToLoad = scene;
    }

    public void OnFadeCommplete() {
        SceneManager.LoadScene(levelToLoad);
    }
}
