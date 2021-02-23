using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [Tooltip("Langsamster Wert für die Bewegung. durch den state-wert 1 abrufbar")]
    public float mov_s = 0.4f;

    [Tooltip("Normaler Wert für die Bewegung. durch den state-wert 2 abrufbar")]
    public float mov_n = 0.75f;

    [Tooltip("Schnellster Wert für die Bewegung. durch den state-wert 3 abrufbar")]
    public float mov_f = 1.15f;

    [Space] [Tooltip("Langsamster Wert für die Auswahl. durch den state-wert 4 abrufbar")]
    public float sel_s = 3;

    [Tooltip("Normaler Wert für die Auswahl. durch den state-wert 5 abrufbar")]
    public float sel_n = 2;

    [Tooltip("Schnellster Wert für die Auswahl. durch den state-wert 6 abrufbar")]
    public float sel_f = 1.15f;

    static float movementSpeed = 0.75f; //Standard Geschwindigkeit
    //Beispiel Presets im Hauptmenü :   Langsam : 0.4f  Standard : 0.75f    Schnell : 1.15f;

    static bool teleportMode = false; //Standardmäßig keine Teleportation

    static float
        selectTime =
            2f; //Zeit, die es dauert, bis eine Auswahl bestätigt ist. Standard : 2; eventuell Preset im Hauptmenü: Schnell : 1.25f     Standard : 2f 

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public void NewGame() {
        SceneManager.LoadScene(1);
    }

    public void ToggleTeleportationOnOff(bool state) {
        teleportMode = state;
    }

    public bool GetTeleportMode() {
        return teleportMode;
    }

    public float GetMovementSpeed() {
        return movementSpeed;
    }

    public float GetSelectTime() {
        return selectTime;
    }

    public void setMovementSpeed(int state) {
        if (state == 1) {
            movementSpeed = mov_s;
        } else if (state == 2) {
            movementSpeed = mov_n;
        } else if (state == 3) {
            movementSpeed = mov_f;
        }
    }

    public void SetSelectSpeed(int state) {
        if (state == 4) {
            selectTime = sel_s;
        } else if (state == 5) {
            selectTime = sel_n;
        } else if (state == 6) {
            selectTime = sel_f;
        }
    }
}