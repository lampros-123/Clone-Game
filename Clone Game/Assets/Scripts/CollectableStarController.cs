using UnityEngine;

public class CollectableStarController : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag=="Clone") {
            GameObject.Find("LevelController").GetComponent<LevelProgressController>().StarCollected();
            gameObject.SetActive(false);
        }
    }

}
