using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour {
    public void OnFadeComplete() {
        LoadScene.instance.OnFadeCommplete();
    }
}
