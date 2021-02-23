using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

//aktiviert / deaktiviert Bereiche der Map
public class ActivateAndDeactivate : MonoBehaviour {
    public List<GameObject> list;

    //wenn nicht active, dann deactivate
    public bool setActive;
    public bool setAtStart;

    bool done = false;

    // Start is called before the first frame update
    void Start() {
        if (setAtStart) {
            SetObjectState();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!setAtStart && !done && other.gameObject.CompareTag("MainCamera")) {
            SetObjectState();
        }
    }

    private void SetObjectState() {
        foreach (GameObject x in list) {
            x.SetActive(setActive);
        }

        done = true;
    }
}