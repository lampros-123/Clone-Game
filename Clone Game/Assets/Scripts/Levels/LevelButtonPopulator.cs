using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonPopulator : MonoBehaviour {
    public GameObject buttonPrefab;
    public Transform buttonParent;

    private ProgressData data;

	void Start () {
        string filePath = Application.persistentDataPath;

        data = ProgressData.GetInstance();

        for (int i = 0; i < data.getNumberOfLevels(); i++) {
            GameObject button = Instantiate(buttonPrefab);
            button.GetComponent<LevelButtonController>().SetLevel(data.GetLevel(i));
            button.transform.SetParent(buttonParent, false);
        }
	}
	
}
