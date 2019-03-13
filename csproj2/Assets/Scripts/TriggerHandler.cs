using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerHandler : MonoBehaviour
{
    public GameObject doorTimeline;
    //public CinemachineVirtualCameraBase thirdPersonCam;
    public CinemachineVirtualCamera vcam1;
    public bool tutorial = true;
    public GameObject welcome_prompt;
    public GameObject objective1_prompt;

    public GameObject objective1;

    public void DoorTrigger()
    {
        Debug.Log("door trigger");
        doorTimeline.SetActive(true);
        StartCoroutine(yeet());
        enabled = false;
    }

    public void Objective1Trigger()
    {
        Debug.Log("objective1 trigger");
        objective1_prompt.SetActive(true);
        objective1.SetActive(true);


    }

    public IEnumerator yeet()
    {

        yield return new WaitForSeconds(.33f);
        vcam1.Priority = 30;
        yield return new WaitForSeconds(3.07f);
        vcam1.Priority = 10;
        if (tutorial)
        {
            welcome_prompt.SetActive(false);
        }
    }


}
