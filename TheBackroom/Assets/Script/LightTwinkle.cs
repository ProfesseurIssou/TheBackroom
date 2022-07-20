using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTwinkle : MonoBehaviour{
    public int nbFrameBetweenReload;
    public float minIntensity;
    public float maxIntensity;

    void Update() {
        if(Time.frameCount % nbFrameBetweenReload == 0) {
            gameObject.GetComponent<Light>().intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
