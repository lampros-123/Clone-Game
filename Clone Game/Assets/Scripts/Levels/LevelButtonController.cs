using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour {
    public Text levelNumberText;
    public GameObject lockedOverlay;


    private Level level;

    public void SetLevel(Level level) {
        this.level = level;


        if(!level.Locked) {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);

            levelNumberText.enabled = true;
            lockedOverlay.SetActive(false);
        } else {
            levelNumberText.enabled = false;
            lockedOverlay.SetActive(true);
            lockedOverlay.GetComponentInChildren<Text>().text = level.GetRequiredStars() + "";
        }
        levelNumberText.text = level.LevelName;

        Transform star1 = transform.Find("StarsCollected/Star1");
        Transform star2 = transform.Find("StarsCollected/Star2");
        Transform star3 = transform.Find("StarsCollected/Star3");

        star1.gameObject.GetComponent<Image>().enabled = (level.Stars & Level.MaskStar1) == Level.MaskStar1;
        star2.gameObject.GetComponent<Image>().enabled = (level.Stars & Level.MaskStar2) == Level.MaskStar2;
        star3.gameObject.GetComponent<Image>().enabled = (level.Stars & Level.MaskStar3) == Level.MaskStar3;
    }

    public void OnClick() {
        GameObject.Find("Canvas").GetComponent<LoadScene>().Load(level.SceneName);
    }
}
