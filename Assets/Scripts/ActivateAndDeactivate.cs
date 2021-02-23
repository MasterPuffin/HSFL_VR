using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ActivateAndDeactivate : MonoBehaviour //aktiviert / deaktiviert Bereiche der Map
{
    public List<GameObject> list;
    public bool setActive; //wenn nicht active, dann deactivate
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