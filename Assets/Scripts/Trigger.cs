using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {

    public bool eventOnlyOnce = true;
    bool eventOnceInvoked = false;
    public UnityEvent e = new UnityEvent();

    private bool isInteractable = true;
    private bool hasInteracted = false;

    SelectCircle img;                           //Auswahlkreis

    // Start is called before the first frame update
    void Start()
    {
        img = GameObject.FindGameObjectWithTag("Circle").GetComponent<SelectCircle>();
    }

    public void StartCircle()
    {
        if (!eventOnceInvoked && isInteractable)
        {
            StopBeingInteractable();
            img.StartCircle(DoThis);
        }
    }

    public void StopCircle()
    {
        if (!hasInteracted)
        {
            img.StopProgress();
            InteractableAgain();
        }
        hasInteracted = false;
    }

    void DoThis()
    {     
        e.Invoke();
        hasInteracted = true;
        if (eventOnlyOnce)
        {
            eventOnceInvoked = true;
        }
        InteractableAgain();
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
