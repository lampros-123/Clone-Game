using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitController : MonoBehaviour
{
    public GameObject progressBar;

    LevelProgressController progressController;
    Animator anim;
    //    Renderer progressRenderer;

    private void Awake() {
        progressController = GameObject.Find("LevelController").GetComponent<LevelProgressController>();
        anim = GetComponent<Animator>();

        //        progressRenderer = progressBar.GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Clone") {
            Destroy(collision.gameObject);
            anim.SetTrigger("Exit");
            Invoke("AddClone", .3f);
        }
    }

    public void AddClone() {
        progressController.AddClone();
    }

}
