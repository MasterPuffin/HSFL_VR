using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectCircle : MonoBehaviour {
    public delegate void Method();

    //Methode, die nach der Auswahl (fillAmount = 1) auszuführen ist
    Method methodToExecute;

    //Kreis (Auswahl) gestartet
    bool progressing = false;

    //Zeit, die man auf das Objekt zum Interagieren gucken muss, wird im GameManager (später Einstellungen hierfür im Hauptmenü) eingestellt 
    float maxTime;

    //Zeitpunkt zu dem der Spieler begonnen hat auf das Objekt zu gucken
    float time = 0;

    Image img;


    // Start is called before the first frame update
    void Start() {
        img = GetComponent<Image>();
        //Kreis nicht gefüllt (0%)
        Debug.Log("start circle");
        img.fillAmount = 0f;
        maxTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GetSelectTime();
    }

    // Update is called once per frame
    void Update() {
        if (progressing) {
            float value = (Time.time - time) / maxTime;
            img.fillAmount = value;

            //wenn der Kreis gefüllt ist
            if (value >= 1) {
                progressing = false;
                methodToExecute();
                img.fillAmount = 0;
            }
        }
    }


    public void StartCircle(Method m) {
        methodToExecute = m;
        img.fillAmount = 0f;
        time = Time.time;
        progressing = true;
    }

    public void StopProgress() {
        //Debug.Log("StopProgress");
        progressing = false;
        img.fillAmount = 0f;
    }

    public void UpdateTime() {
        maxTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GetSelectTime();
    }
}