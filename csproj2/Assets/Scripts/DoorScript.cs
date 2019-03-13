using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DoorScript : MonoBehaviour {

    public GameObject timeline;
    //public CinemachineVirtualCameraBase thirdPersonCam;
    public CinemachineVirtualCamera vcam1;
    public bool tutorial = true;
    public GameObject welcome_prompt;

    private void OnTriggerEnter(Collider other)
    {
        timeline.SetActive(true);
        StartCoroutine(yeet());
        enabled = false;
    }

    public IEnumerator yeet()
    {

        yield return new WaitForSeconds(.33f);
        vcam1.Priority = 30;
        yield return new WaitForSeconds(3.07f);
        vcam1.Priority = 10;
        if(tutorial)
        {
            welcome_prompt.SetActive(false);
        }
    }
}
