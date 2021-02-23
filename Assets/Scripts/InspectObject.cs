﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InspectObject : MonoBehaviour  //es gibt noch kein richtiges Aufheben. Aktuell wird nur das Objekt deaktiviert, wenn es angeguckt wurde
{
    public bool doMoveObject=true;

    [Space]
    public bool eventOnlyOnce = true;
    bool eventInvoked = false;
    public UnityEvent onInspectEnding = new UnityEvent();

    private Vector3 originPosition;         //original Position des Objekts, um es nach dem Angucken wieder an diese Position zu bringen
    private Quaternion originRotation;      //original Rotation 
    Vector3 inspectPos;                     //die Position zu der das Objekt bewegt wird, um es besser angucken zu können

    [Space]
    public float translateTime;             //Zeit, die es braucht bis das Objekt an der Position (inspectPos) angekommen ist, wo der Spieler es betrachten kann
    public float timeToInspect;             //Zeit zum Angucken

    [Space]
    [Tooltip("Wert zwischen 0 und 1 : wie nah ist das Objekt an der Kamera")]
    [Range(0.0f, 1.0f)]
    public float inspectDistance = 0.5f;    //Wert zwischen 0 und 1 : wie nah ist das Objekt an der Kamera. 1 = selbe Position, 0 = original Position aber Höhe der Kamera
    [Tooltip("Wert zwischen 0 und 1 : auf welcher Höhe soll das Objekt betrachtet werden. 1 = Höhe der Kamera")]
    [Range(0.0f, 1.0f)]
    public float inspectHeight = 0.85f;      //Wert zwischen 0 und 1 : auf welcher Höhe soll das Objekt betrachtet werden. 1 = Höhe der Kamera (Sicht des Spielers), 0 = die gleiche Höhe auf der das Objekt ursprünglich war

    bool isInteractable = true;             //interagierbar
    bool alreadyNotified = false;
    bool canInspect = false;                //kann das Objekt angeguckt werden? True, wenn das Objekt durchs Angucken ausgewählt wurde  
    bool isOnInspectPosition = false;       
    bool inspected = false;
    SelectCircle img;                       //Auswahlkreis

    Transform cam; //MainCamera

    float i; //Wert zwischen 0 und 1 für die Transition von Position a zu b (0 bis 1)
    float j; //Wert zwischen 0 und 1 für die Rotationtransition

    //public GameObject 

    private void Awake()
    {

        originPosition = gameObject.transform.position;
        originRotation = gameObject.transform.rotation;
        
    }

    // Start is called before the first frame update
    private void Start()
    {        
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        img = GameObject.FindGameObjectWithTag("Circle").GetComponent<SelectCircle>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canInspect)
        {
            if (doMoveObject)
            {
                ShowObject();
            }
            else
            {
                OnEndRotation();
                OnEndPosition();
            }
        }

    }

    public void ShowObject()
    {
        if (!isOnInspectPosition && !inspected)
        {
            TransitionToPosition(originPosition, inspectPos);
        }
        else if (isOnInspectPosition && !inspected)
        {
            RotateObject();
        }
        else if (inspected)
        {
            TransitionToPosition(inspectPos, originPosition);

        }
    }

    public void StartCircle()
    {
        if (isInteractable && !canInspect)
        {
            StopBeingInteractable();
            img.StartCircle(InspectTransition);
        }
    }

    public void StopCircle()
    {
        if (!canInspect)
        {
            img.StopProgress();
            InteractableAgain();
        }
    }


    public void InspectTransition()
    {

        canInspect = true;
        inspected = false;
        isOnInspectPosition = false;

        Vector3 vec = Vector3.Lerp(originPosition, cam.position, inspectDistance);
        Vector3 vecHeight = Vector3.Lerp(originPosition, cam.position, inspectHeight);

        inspectPos = new Vector3(vec.x, vecHeight.y, vec.z);
        StopBeingInteractable();

    }

    private void TransitionToPosition(Vector3 startPosition, Vector3 endPosition)
    {
        if (i < 1)
        {
            i += Time.deltaTime * (1f/translateTime);
            transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0f, 1f, i));

            if (i >= 1)
            {
                i = 1;
                OnEndPosition();
            }
        }
    }

    private void OnEndPosition()
    {

        isOnInspectPosition = !isOnInspectPosition;
        if (!isOnInspectPosition)
        {
            canInspect = false;
            InteractableAgain();
        }
        i = 0;
    }


    private void RotateObject()
    {
        //Quaternion.Lerp(transform.localRotation, endRotation, j);     //Rotation von einem Quaternion zum nächsten. Wurde erstmal nicht weitergemacht
        //= Vector3.Lerp(startRotation, endRotation, Mathf.SmoothStep(0f, 1f, j));            

        if (j < 1)      //kann besser gemacht werden, aktuell wird sich einfach nur einmal umgedreht. Objekte müssen dafür ausgelegt sein (Pivot Point usw.). Aktuell nicht immer der Fall
        {
            float rot = j * 360;
            j += Time.deltaTime * (1f / timeToInspect);
            rot = (j * 360) - rot;
            transform.Rotate(new Vector3(0, rot, 0), Space.Self);

            if (j >= 1)
            {
                j = 1;
                OnEndRotation();
            }
        }
        
    }

    private void OnEndRotation()
    {
        inspected = true;
        j = 0;
        i = 0;

        if (onInspectEnding != null && !eventInvoked)
        {
            onInspectEnding.Invoke();

            if (eventOnlyOnce)
            {
                eventInvoked = true;
            }
        }
        /*
        if (doMoveObject)
        {
            canInspect = true;
            inspected = false;
        }
        */

    }

    private void StopBeingInteractable()
    {
        isInteractable = false;
    }

    private void InteractableAgain()
    {
        isInteractable = true;
    }


}