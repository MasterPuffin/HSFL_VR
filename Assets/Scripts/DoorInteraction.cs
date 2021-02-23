using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class DoorInteraction : MonoBehaviour {
    public bool eventOnlyOnce = true;
    bool eventInvoked = false;
    public UnityEvent eventAfterAnimation = new UnityEvent();


    [Space] Animator anim;

    public string
        boolName = "Open"; //boolean variable, die auf true oder false gesetzt werden muss, um eine Aniamtion auszuführen

    int boolID;
    public bool open = false; //Tür auf oder zu (zum Start des Spiels)


    [Space]
    public bool canBeOpened = true; //bedeutet man hat bereits einen Schlüssel für die Tür oder man braucht keinen

    public bool isInteractable = true;
    public bool interactOnlyOnce = true;
    [Space] [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip doorOpenClip;
    [SerializeField] protected AudioClip doorCloseAndLockClip;
    bool isAnimating = false;
    private float closeTime = 5;
    SelectCircle img;

    private void Awake() {
        anim = GetComponent<Animator>();
        boolID = Animator.StringToHash(boolName); //einfacher über eine ID als einem String
        anim.SetBool(boolID, open);
    }


    // Start is called before the first frame update
    void Start() {
        img = GameObject.FindGameObjectWithTag("Circle").GetComponent<SelectCircle>();
    }

    public void StartCircle() {
        if (isInteractable && canBeOpened && !isAnimating) {
            StopBeingInteractable();
            img.StartCircle(StartAnimation);
        }
    }

    public void StopCircle() {
        if (!isAnimating) {
            img.StopProgress();
            InteractableAgain();
        }
    }

    public void StartAnimation() {
        open = !open;
        anim.SetBool(boolID, open);
        isAnimating = true;
        audioSource.PlayOneShot(doorOpenClip);
    }

    public void AfterAnimation() {
        isAnimating = false;
        if (eventAfterAnimation != null && !eventInvoked) {
            eventAfterAnimation.Invoke();
            if (eventOnlyOnce) {
                eventInvoked = true;
            }
        }

        if (!interactOnlyOnce) {
            InteractableAgain();
            audioSource.PlayOneShot(doorOpenClip);
        }
    }

    public void SetDoorOpen(bool value) {
        open = value;
        anim.SetBool(boolID, open);
        isAnimating = true;

        audioSource.PlayOneShot(doorCloseAndLockClip);
    }

    public void CantBeOpened() {
        canBeOpened = false;
    }

    public void CanOpen() {
        canBeOpened = true;
    }

    private void StopBeingInteractable() {
        isInteractable = false;
    }

    private void InteractableAgain() {
        isInteractable = true;
    }
}