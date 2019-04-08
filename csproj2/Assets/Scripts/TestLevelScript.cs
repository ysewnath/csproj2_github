using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestLevelScript : MonoBehaviour
{
    public GameObject vcam12;
    CinemachineVirtualCamera vcam12Handler;

    // Use this for initialization
    void Start()
    {
        vcam12Handler = vcam12.GetComponent<CinemachineVirtualCamera>();
        enabled = false;
    }

    public void EnterVcam12()
    {
        vcam12Handler.Priority = 40;

    }

    public void ExitVcam12()
    {
        vcam12Handler.Priority = 10;

    }



}
