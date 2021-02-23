using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchTrigger : MonoBehaviour {
    public bool useTrigger = false;

    [Tooltip("Events, die passieren sollen wenn useTrigger = true")]
    public UnityEvent eventTrigger = new UnityEvent();

    [Space] [Tooltip("Events, die passieren sollen wenn useTrigger = false")]
    public UnityEvent eventElse = new UnityEvent();


    public void ActivateEvent() {
        if (useTrigger) {
            if (eventTrigger != null) {
                eventTrigger.Invoke();
            }
        } else {
            if (eventElse != null) {
                eventElse.Invoke();
            }
        }
    }

    public void SetState(bool newUseTrigger) {
        useTrigger = newUseTrigger;
    }
}