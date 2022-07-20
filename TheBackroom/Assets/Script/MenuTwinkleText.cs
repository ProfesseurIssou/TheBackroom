using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTwinkleText : MonoBehaviour{
    public int nbFrameBetweenState;
    public int nbFrameBetweenStateAfterEnter;

    private bool state = true;

    public GameObject UIText;

    void Update() {
        if(Input.GetButtonDown("Submit")) {
            nbFrameBetweenState = nbFrameBetweenStateAfterEnter;
        };
        if(Time.frameCount % nbFrameBetweenState == 0) {
            state = !state;
            UIText.SetActive(state);
        }
    }
}
