using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    bool teleport = false;

    Vector3 startPosition;
    Vector3 endPosition;

    //Spieler bewegt sich gerade
    bool move = false;

    //Wert zwischen 0 und 1
    float i = 0;

    //wird im GameManager(später Einstellungen hierfür im Hauptmenü) eingestellt
    float movementSpeed;

    //movementSpeed / Länge der Strecke startPosition bis endPosition
    float speedPerUnit;

    [Tooltip("PositionsPunkte auf dem Boden")]
    public GameObject PositionPoints;

    public TeleportPlayer teleportP;

    private void Start() {
        movementSpeed = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>()
            .GetMovementSpeed();
        teleport = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GetTeleportMode();
    }

    // Update is called once per frame
    void Update() {
        if (move) {
            if (teleport) {
                //erst wenn der Bildschirm schwarz ist für den Spieler, soll der Player bewegt werden
                if (teleportP.GetCanMove()) {
                    TeleportMovement();
                }
            } else {
                if (i < 1) {
                    i += Time.deltaTime * speedPerUnit;
                    transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0f, 1f, i));

                    if (i >= 1) {
                        i = 1;
                        ReachEndPosition();
                    }
                }
            }
        }
    }

    public void NewPosition(Vector3 newPosition) {
        //alle Positionspunkte auf dem Boden werden deaktiviert, um sie nicht während der Bewegung zu sehen. Führt zu "PointerExit has no receiver!",  
        //da OnHoverExit() aus CardboardInteractable eine Methode von einem deaktivierten Objekt aufrufen will. Führt nicht zu abstürzen.
        //Man könnte die Positionspunkte einen Frame später deaktivieren oder die Objekte anders unsichtbar machen, um das Problem zu lösen
        PositionPoints.SetActive(false);
        startPosition = transform.position;
        endPosition = newPosition + new Vector3(0, transform.localScale.y, 0);

        speedPerUnit = movementSpeed / Vector3.Distance(startPosition, endPosition);
        i = 0;
        move = true;

        if (teleport) {
            teleportP.StartTransition();
        }
    }


    public void ReachEndPosition() {
        transform.position = endPosition;
        move = false;
        //macht die Punkte auf dem Boden wieder sichtbar, wenn der Spieler an der Zielposition angekommen ist
        PositionPoints.SetActive(true);
    }

    public bool IsMoving() {
        return move;
    }

    //es kann nicht einfach teleportiert werden, da sonst die Trigger nicht getriggert werden können. Deshalb wurden die Trigger größer gemacht 
    //und der Spieler bewegt sich schnell von a nach b während das Bild für den Spieler kurz schwarz ist
    void TeleportMovement() {
        if (i < 1) {
            i += Time.deltaTime * 5.5f;
            transform.position = Vector3.Lerp(startPosition, endPosition, i);

            if (i >= 1) {
                i = 1;
                teleportP.FadeIn();
                ReachEndPosition();
            }
        }
    }

    public void UpdateMovement() {
        teleport = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GetTeleportMode();
    }

    public void UpdateMovementSpeed() {
        movementSpeed = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>()
            .GetMovementSpeed();
    }
}