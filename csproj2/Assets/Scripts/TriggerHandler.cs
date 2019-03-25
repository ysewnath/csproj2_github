using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.AI;

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

    public GameObject station2InteractScript;
    public GameObject station2Script;
    StationInteractScript station2InteractScriptHandler;
    StationScript station2ScriptHandler;

    public GameObject station3InteractScript;
    public GameObject station3Script;
    StationInteractScript station3InteractScriptHandler;
    StationScript station3ScriptHandler;

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
    public GameObject objective2;
    public GameObject objective3;
    public int prompt = 0;

    public GameObject stationFinishedDialog1;
    public GameObject stationFinishedDialog2;

    public GameObject detectedScript;
    DetectedHandler detectedScript_handler;

    public GameObject player;
    MovementInput playerHandler;
    private NavMeshAgent mNavMeshAgent;
    bool enableNavMesh = false;
    Vector3 waypoint1 = new Vector3(-3, 3, 116.68f);


    private void Start()
    {
        doorTriggerHandler = doorTrigger.GetComponent<EventTrigger>();
        SlowmotionHandler = TimeHandler.GetComponent<TimeHandler>();
        battledroid1Handler = battledroid1.GetComponent<BattledroidHandler>();

        stationScriptHandler = stationScript.GetComponent<StationScript>();
        stationInteractScriptHandler = stationsInteractScript.GetComponent<StationInteractScript>();

        station2InteractScriptHandler = station2InteractScript.GetComponent<StationInteractScript>();
        station2ScriptHandler = station2Script.GetComponent<StationScript>();
        station3InteractScriptHandler = station3InteractScript.GetComponent<StationInteractScript>();
        station3ScriptHandler = station3Script.GetComponent<StationScript>();

        detectedScript_handler = detectedScript.GetComponent<DetectedHandler>();
        playerHandler = player.GetComponent<MovementInput>();
        mNavMeshAgent = player.GetComponent<NavMeshAgent>();

        // for tutorial
        stationScriptHandler.locked = true;
        stationInteractScriptHandler.locked = true;

        station2InteractScriptHandler.locked = true;
        station2ScriptHandler.locked = true;

        station3InteractScriptHandler.locked = true;
        station3ScriptHandler.locked = true;

        detectedScript_handler.numStations = 3;
        //mNavMeshAgent.enabled = false;


        battledroidTimelineHandler = battledroidTimeline.GetComponent<PlayableDirector>();
        battledroidTimelineHandler.timeUpdateMode = DirectorUpdateMode.UnscaledGameTime;
        //enabled = false;
    }

    private void Update()
    {
        //
        // look for user input
        //
        if(enableNavMesh)
        {
            GoToLocation();
        }
        else if (Input.GetButtonDown("Interact2"))
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
                prompt = 3;
                Debug.Log("prompt = " + prompt);
            }
            else if(prompt == 3)
            {
                battledroid_prompt2.SetActive(false);
                //
                // stop slowmotion
                //
                prompt = 4;
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
                prompt = 5;
                Debug.Log("prompt = " + prompt);
            }
            else if(prompt == 6)
            {
                stationTutorialPrompt1.SetActive(false);
                stationTutorialPrompt2.SetActive(true);
                prompt = 7;
                Debug.Log("prompt = " + prompt);
            }
            else if(prompt == 7)
            {
                stationTutorialPrompt2.SetActive(false);
                stationTutorialPrompt3.SetActive(true);
                prompt = 8;
                Debug.Log("prompt = " + prompt);
            }
            else if (prompt == 8)
            {
                stationTutorialPrompt3.SetActive(false);
                stationInteractScriptHandler.tutorialInProgress = false;
                stationInteractScriptHandler.tutorial = false;
                stationInteractScriptHandler.tutorial2 = true;

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
            else if(prompt == 9)
            {
                stationFinishedDialog1.SetActive(false);
                stationFinishedDialog2.SetActive(true);
                prompt = 10;
                Debug.Log("prompt = " + prompt);


            }
            else if(prompt == 10)
            {

                stationFinishedDialog2.SetActive(false);
                station2ScriptHandler.locked = false;
                station2InteractScriptHandler.locked = false;
                station3ScriptHandler.locked = false;
                station3InteractScriptHandler.locked = false;
                objective1.SetActive(false);
                objective2.SetActive(true);
                objective3.SetActive(true);
                station3InteractScriptHandler.anim.SetBool("isKneeling", false);
                station3InteractScriptHandler.tutorial2 = false;
                prompt = 11;
                Debug.Log("prompt = " + prompt);


            }

        }
    }

    public void GoToLocation()
    {
        //mNavMeshAgent.enabled = true;
        mNavMeshAgent.destination = waypoint1;

        if (Vector3.Distance(waypoint1, this.transform.position) < .5f)
        {
            mNavMeshAgent.velocity = new Vector3(0, 0, 0);
            enableNavMesh = false;
            stationInteractScriptHandler.anim.SetBool("isPassingThrough", false);

        }
        else
        {
            stationInteractScriptHandler.anim.SetBool("isPassingThrough", true);
        }



    }

    public void DoorTrigger2()
    {
        //
        // play cutscene and animation
        //
        detectedScript_handler.isInteracting = true;
        Debug.Log("doorTrigger2");
        playerHandler.moveLock = true;
        mNavMeshAgent.Warp(player.transform.position);
        enableNavMesh = true;


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
            prompt = 2;
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
            prompt =1;
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
