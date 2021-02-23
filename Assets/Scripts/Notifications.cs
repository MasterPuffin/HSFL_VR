using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notifications : MonoBehaviour {
    public Text notification;
    public float timeOnScreen = 5.0f;

    [Space] public string text_takeObject = "I found something. Maybe i can use it somewhere.";

    private bool isTimerAktive = false;
    private float time = 0;
    private string text_none = " ";

    private void Update() {
        if (isTimerAktive) {
            time += Time.deltaTime;
            if (time >= timeOnScreen) {
                StopNotification();
            }
        }
    }

    private void StopNotification() {
        notification.text = text_none;
        time = 0;
        isTimerAktive = false;
    }

    public void SendNotification() {
        notification.text = text_takeObject;
        isTimerAktive = true;
    }

    public void SendNotificationWithCustomText(string text_custom) {
        notification.text = text_custom;
        isTimerAktive = true;
    }
}