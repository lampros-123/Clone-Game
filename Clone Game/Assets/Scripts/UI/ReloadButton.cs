using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadButton : MonoBehaviour {

	public void Start () {
        GetComponent<Button>().onClick.AddListener(Reload);
	}
	
    public void Reload() {
        string sceneName = SceneManager.GetActiveScene().name;
        GameObject.Find("StandardLevelHUD").GetComponent<LoadScene>().Load(sceneName);
    }
}
