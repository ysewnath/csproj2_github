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
    bool objective1activated = false;

    public GameObject objective1Trigger;

    public GameObject stationScript;
    StationScript stationScriptHandler;

    public GameObject stationsInteractScript;
    StationInteractScript stationInteractScriptHandler;

    public GameObject stationTutorialPrompt1;
    public GameObject stationTutorialPrompt2;
    public GameObject stationTutorialPrompt3;

    public GameObject TimeHandler;
    TimeHandler SlowmotionHandler;

    public GameObject battledroidTimeline;
    PlayableDirector battledroidTimelineHandler;

    public GameObject battledroid_prompt1;
    public GameObject battledroid_prompt2;

    public GameObject battledroid1;
    BattledroidHandler battledroid1Handler;

    public GameObject objective1;
    public int prompt = 0;

    private void Start()
    {
        doorTriggerHandler = doorTrigger.GetComponent<EventTrigger>();
        SlowmotionHandler = TimeHandler.GetComponent<TimeHandler>();
        battledroid1Handler = battledroid1.GetComponent<BattledroidHandler>();

        stationScriptHandler = stationScript.GetComponent<StationScript>();
        stationInteractScriptHandler = stationsInteractScript.GetComponent<StationInteractScript>();

        // for tutorial
        stationScriptHandler.locked = true;
        stationInteractScriptHandler.locked = true;

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
                Debug.Log("prompt = " + prompt);
            }
            else if(prompt == 1)
            {
                objective1_prompt.SetActive(false);
                objective1Trigger.SetActive(false);
                Debug.Log("prompt = " + prompt);
            }
            else if(prompt == 2)
            {
                battledroid_prompt1.SetActive(false);
                battledroid_prompt2.SetActive(true);
                prompt++;
                Debug.Log("prompt = " + prompt);
            }
            else if(prompt == 3)
            {
                battledroid_prompt2.SetActive(false);
                //
                // stop slowmotion
                //
                prompt++;
                battledroid1Handler.returnToPosition = true;
                battledroid1Handler.enabled = true;
                BattledroidInfo(false);
                stationScriptHandler.locked = false;
                stationInteractScriptHandler.locked = false;
                stationInteractScriptHandler.tutorial = true;
                objective1_prompt.SetActive(true);
                Debug.Log("prompt = " + prompt);
            }
            else if(prompt == 4)
            {
                objective1_prompt.SetActive(false);
                prompt++;
                Debug.Log("prompt = " + prompt);
            }
            else if(prompt == 6)
            {
                stationTutorialPrompt1.SetActive(false);
                stationTutorialPrompt2.SetActive(true);
                prompt++;
                Debug.Log("prompt = " + prompt);
            }
            else if(prompt == 7)
            {
                stationTutorialPrompt2.SetActive(false);
                stationTutorialPrompt3.SetActive(true);
                prompt++;
                Debug.Log("prompt = " + prompt);
            }
            else if (prompt == 8)
            {
                stationTutorialPrompt3.SetActive(false);
                stationInteractScriptHandler.tutorialInProgress = false;
                stationInteractScriptHandler.tutorial = false;

                prompt++;

                // populate the 3 pages of the question dialog
                // start at page 1
                stationInteractScriptHandler.currentPage = 1;
                stationInteractScriptHandler.currentSelection = 1;
                stationInteractScriptHandler.option_textHandler[0].rectTransform.localScale = new Vector3(.55f, .55f, .55f);
                stationInteractScriptHandler.PopulateDialog();
                stationInteractScriptHandler.interact = true;
                stationInteractScriptHandler.DialogBox.SetActive(true);
                Debug.Log("prompt = " + prompt);
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
        if(!objective1activated)
        {
            Debug.Log("objective1 trigger");
            objective1_prompt.SetActive(true);
            objective1.SetActive(true);
            prompt++;
            objective1activated = true;
        }
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
