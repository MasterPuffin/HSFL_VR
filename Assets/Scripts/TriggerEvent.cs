using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour {
    public UnityEvent onTriggerEnter = new UnityEvent();
    public bool onlyOnce = true;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");
        if (other.gameObject.CompareTag("MainCamera")) {
            if (onTriggerEnter != null) {
                onTriggerEnter.Invoke();
            }

            Debug.Log("Trigger Camera Event Invoked");
            if (onlyOnce) {
                gameObject.SetActive(false);
            }
        }
    }
}