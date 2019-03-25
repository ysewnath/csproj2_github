using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedHandler : MonoBehaviour
{

    public bool detected = false;
    public bool searchDetected = false;
    public bool isInteracting = false;

    public int numCorrect = 0;
    public int numStations = 0;
    public int stationIndex = 0;

    public bool winCondition = false;

    public GameObject findStations_Dialog;
    public GameObject findStations_collider;

    public void ShowRequirement(bool option)
    {
        Debug.Log("toggling FindStation prompt");
        if (option)
            findStations_Dialog.SetActive(true);
        else
            findStations_Dialog.SetActive(false);


    }


}
