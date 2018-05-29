using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelProgressController : MonoBehaviour {
    public int requiredClones = 3;
    public float starTime = 10f;

    int collectedClones = 0;


    Text progressText;
    Text requiredText;
    Animator progressTextAnim;
    Animator completedPopupAnim;
    ICloneController controller;
    RectTransform timeStarTransform;
    float timeStarStartHeight;

    bool completedStar = false;
    bool timeStar = false;
    bool collectableStar = false;

    public void Start() {
        progressText = GameObject.Find("ProgressText").GetComponent<Text>();
        requiredText = GameObject.Find("RequiredText").GetComponent<Text>();
        progressTextAnim = GameObject.Find("ProgressDisplay").GetComponent<Animator>();
        completedPopupAnim = GameObject.Find("LevelCompletedPopup").GetComponent<Animator>();
        controller = GetComponent<ICloneController>();
        timeStarTransform = (RectTransform) GameObject.Find("TimeStarMask").GetComponent<Transform>();
        timeStarStartHeight = timeStarTransform.sizeDelta.y;

        progressText.text = "0";
        requiredText.text = " / " + requiredClones;
    }

    public void Update() {
        float time = Time.fixedDeltaTime * controller.GetCurrentFrame();
        float percent = time / starTime;

        Vector3 sizeDelta = timeStarTransform.sizeDelta;
        sizeDelta.y = timeStarStartHeight - timeStarStartHeight * percent;
        if (sizeDelta.y < 0) sizeDelta.y = 0;

        timeStarTransform.sizeDelta = sizeDelta;
    }

    public void AddClone() {
        collectedClones++;
        progressText.text = "" + collectedClones;

        if(requiredClones == collectedClones) {
            LevelCompleted();
        }
    }
   
    public void LevelCompleted() {
        progressTextAnim.SetTrigger("Completed");
        completedPopupAnim.SetTrigger("Completed");
        GameObject.Find("PauseButton").SetActive(false);

        completedStar = true;

        if (Time.fixedDeltaTime * controller.GetCurrentFrame() <= starTime)
            timeStar = true;

        int nbStars = 0;
        if (completedStar) nbStars++;
        if (timeStar) nbStars++;
        if (collectableStar) nbStars++;

        Level level = ProgressData.GetInstance().GetLevelBySceneName(SceneManager.GetActiveScene().name);
        level.Complete(nbStars);
        ProgressData.GetInstance().SaveProgress();

        GameObject star1 = GameObject.Find("Star1");
        GameObject star2 = GameObject.Find("Star2");
        GameObject star3 = GameObject.Find("Star3");

        star1.SetActive(completedStar);
        star2.SetActive(timeStar);
        star3.SetActive(collectableStar);
    }
}
