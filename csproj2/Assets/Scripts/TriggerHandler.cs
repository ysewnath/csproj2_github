using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class TriggerHandler : MonoBehaviour
{
    public GameObject doorTimeline;
    //public CinemachineVirtualCameraBase thirdPersonCam;
    public CinemachineVirtualCamera vcam1;
    public bool tutorial = true;
    public GameObject welcome_prompt;
    public GameObject objective1_prompt;
    public GameObject doorTrigger;
    EventTrigger doorTriggerHandler;

    public GameObject TimeHandler;
    TimeHandler SlowmotionHandler;

    public GameObject battledroidTimeline;
    PlayableDirector battledroidTimelineHandler;

    public GameObject battledroid_prompt1;
    public GameObject battledroid_prompt2;

    public GameObject battledroid1;
    BattledroidHandler battledroid1Handler;

    public GameObject objective1;
    int prompt = 0;

    private void Start()
    {
        doorTriggerHandler = doorTrigger.GetComponent<EventTrigger>();
        SlowmotionHandler = TimeHandler.GetComponent<TimeHandler>();
        battledroid1Handler = battledroid1.GetComponent<BattledroidHandler>();

        battledroidTimelineHandler = battledroidTimeline.GetComponent<PlayableDirector>();
        battledroidTimelineHandler.timeUpdateMode = DirectorUpdateMode.UnscaledGameTime;
        //enabled = false;
    }

    private void Update()
    {
        //
        // look for user input
        //
        if (Input.GetButtonDown("Interact2"))
        {
            //
            // dissmiss tutorial dialog prompts
            //
            if(prompt == 0)
            {
                welcome_prompt.SetActive(false);
            }
            else if(prompt == 1)
            {
                objective1_prompt.SetActive(false);
            }
            else if(prompt == 2)
            {
                battledroid_prompt1.SetActive(false);
                battledroid_prompt2.SetActive(true);
                prompt++;
            }
            else if(prompt == 3)
            {
                battledroid_prompt2.SetActive(false);
                //
                // stop slowmotion
                //
                prompt++;
                battledroid1Handler.enabled = true;
                BattledroidInfo(false);
            }

        }
    }

    public void Objective1_end()
    {
        objective1_prompt.SetActive(false);

    }

    public void BattledroidInfo(bool option)
    {
        if(option)
        {
            SlowmotionHandler.SlowmotionHandler(true);
            //
            // display dialog
            //
            battledroid_prompt1.SetActive(true);
            prompt++;
        }
        else
        {
            SlowmotionHandler.SlowmotionHandler(false);
            //
            // hide dialog
            //
        }

    }

    public void DoorTrigger()
    {
        Debug.Log("door trigger");
        doorTriggerHandler.isTrigger = false;
        doorTimeline.SetActive(true);
        StartCoroutine(yeet());
        
    }

    public void Objective1Trigger()
    {
        Debug.Log("objective1 trigger");
        objective1_prompt.SetActive(true);
        objective1.SetActive(true);
        prompt++;


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
