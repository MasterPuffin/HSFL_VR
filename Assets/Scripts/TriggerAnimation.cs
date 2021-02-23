using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAnimation : MonoBehaviour
{

    public bool eventOnlyOnce = true;                                   
    bool eventInvoked = false;
    public UnityEvent eventAfterAnimation = new UnityEvent();   //Event, das nach der Animation ausgeführt werden soll

    [Space]
    Animator anim;
    public string boolName;                 //boolean Variable (Name), die auf true oder false gesetzt werden muss, um eine Aniamtion auszuführen
    int boolID;                             //ID der boolean Variable

    [Space]
    public bool isInteractable = true;
    public bool interactOnlyOnce;

    
    bool isAnimating = false;
    SelectCircle img;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boolID = Animator.StringToHash(boolName);           //einfacher und schneller über eine ID als einem String auf die Variable zuzugreifen
        img = GameObject.FindGameObjectWithTag("Circle").GetComponent<SelectCircle>();
    }

    public void StartCircle()
    {
        if (isInteractable && !isAnimating)
        { 
            StopBeingInteractable();
            img.StartCircle(StartAnimation);
        }
    }

    public void StopCircle()
    {
        if (!isAnimating)
        {
            img.StopProgress();
            InteractableAgain();
        }
    }

    public void StartAnimation()
    {
        anim.SetBool(boolID, true);
        isAnimating = true;
    }

    public void StopAnimation()
    {
        anim.SetBool(boolID, false);
        isAnimating = false;
    }

    public void AfterAnimation()
    {       

        if (eventAfterAnimation != null && !eventInvoked)           
        {
            eventAfterAnimation.Invoke();
            if (eventOnlyOnce)
            {
                eventInvoked = true;
            }
        }        

        if (!interactOnlyOnce)
        {
            InteractableAgain();
        }
    }

    public void SetBool(bool value)
    {
        anim.SetBool(boolID, value);
    }

    private void StopBeingInteractable()
    {
        isInteractable = false;
    }

    private void InteractableAgain()
    {
        isInteractable = true;
    }


    public void StartToBeInteractble()
    {
        isInteractable = true;
    }

}
