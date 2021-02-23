using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{

    public bool onStartEnabled;             //Status des Lichts zu Beginn des Spiels (An/ Aus)
    Light l;

    [Space]
    public float seconds;
    public bool turnOnAfterSeconds = true;  //ob nach Ablauf der Sekunden das Licht angeschaltet werden soll (true) oder Aus (false)


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        l = GetComponent<Light>();
        l.enabled = onStartEnabled;
    }

    public void TurnLightsOnOff()
    {

        StartCoroutine(TurnLightOnOffAfterSeconds());
    }
    
    public IEnumerator TurnLightOnOffAfterSeconds()
    {
        
        yield return new WaitForSeconds(seconds);
        l.enabled = turnOnAfterSeconds;

    }


    public void SetLight(bool lightOn)
    {
        l.enabled = lightOn;
    }
    
}
