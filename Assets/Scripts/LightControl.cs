using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {
    //Status des Lichts zu Beginn des Spiels (An/ Aus)
    public bool onStartEnabled;
    Light l;

    [Space] public float seconds;

    //ob nach Ablauf der Sekunden das Licht angeschaltet werden soll (true) oder Aus (false)
    public bool turnOnAfterSeconds = true;


    // Start is called before the first frame update
    void Start() {
        Debug.Log("Start");
        l = GetComponent<Light>();
        l.enabled = onStartEnabled;
    }

    public void TurnLightsOnOff() {
        StartCoroutine(TurnLightOnOffAfterSeconds());
    }

    public IEnumerator TurnLightOnOffAfterSeconds() {
        yield return new WaitForSeconds(seconds);
        l.enabled = turnOnAfterSeconds;
    }


    public void SetLight(bool lightOn) {
        l.enabled = lightOn;
    }
}