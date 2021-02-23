using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositions : MonoBehaviour
{

    public List<PlayerPositions> positionsNearby;   //die von dieser Position aus sichtbaren Punkte

    PlayerMovement playerM;

    GameObject allPositionPoints;

    private bool isInteracting = false; //Spieler guckt auf den Positionspunkt, dann true
    SelectCircle img;                   //Auswahlkreis

    // Start is called before the first frame update
    private void Start()
    {
        img = GameObject.FindGameObjectWithTag("Circle").GetComponent<SelectCircle>();
        playerM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        allPositionPoints = transform.parent.gameObject;
    }


    public void StartCircle()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            img.StartCircle(SetNewPosition);
        }
    }

    public void StopCircle()
    {
        if (isInteracting)
        {
            img.StopProgress();
            isInteracting = false;
        }
    }

    private void SetNewPosition()
    {
        isInteracting = false;
        ChangePositionPointsVisibility();
        playerM.NewPosition(transform.position);    //übergibt dem PlayerMovement die Koordinaten für diesen PositionsPunkt, wo der Spieler jetzt hinlaufen will
        gameObject.SetActive(false);
    }



    void ChangePositionPointsVisibility()
    {

        for (int i=0; i < allPositionPoints.transform.childCount; i++)              //alle Positionen werden deaktiviert
        {
            GameObject child = allPositionPoints.transform.GetChild(i).gameObject;
            if(!child.Equals(gameObject))
            {
                child.SetActive(false);
            } else
            {
                Debug.Log("this gameObject isnt deactivated!");
            }          
        }

        foreach (PlayerPositions p in positionsNearby)                      //nur die von dieser Position aus sichtbaren Positionen werden wieder aktiviert und damit sichtbar. Da in PlayerMovement gleichzeitig
        {                                                                   //das Parent "PlayerPositions" deaktiviert wird, sieht man die neu aktivierten Punkte erst, wenn der Spieler am nächsten Punkt angekommen ist         
            p.gameObject.SetActive(true);
            Debug.Log("Activate nearby Positions!");
        }
    }

}
