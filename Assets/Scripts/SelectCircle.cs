using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectCircle : MonoBehaviour
{

    public delegate void Method();
    Method methodToExecute;             //Methode, die nach der Auswahl (fillAmount = 1) auszuführen ist

    bool progressing = false;           //Kreis (Auswahl) gestartet

    float maxTime;                      //Zeit, die man auf das Objekt zum Interagieren gucken muss, wird im GameManager (später Einstellungen hierfür im Hauptmenü) eingestellt 
    float time = 0;                     //Zeitpunkt zu dem der Spieler begonnen hat auf das Objekt zu gucken

    Image img;


    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.fillAmount = 0f;            //Kreis nicht gefüllt (0%)
        maxTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GetSelectTime();
    }

    // Update is called once per frame
    void Update()
    {

        if (progressing)
        {
            float value = (Time.time - time) / maxTime;
            img.fillAmount = value;

            if (value >= 1)             //wenn der Kreis gefüllt ist
            {
                progressing = false;
                methodToExecute();
                img.fillAmount = 0;
            }
        }
    }


    public void StartCircle(Method m)
    {
        methodToExecute = m;
        img.fillAmount = 0f;
        time = Time.time;
        progressing = true;
    }

    public void StopProgress()
    {
        Debug.Log("StopProgress");
        progressing = false;
        img.fillAmount = 0f;
    }

    public void UpdateTime()
    {
        maxTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GetSelectTime();
    }
}


