using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using System;

public class StationInteractScript : MonoBehaviour
{

    public GameObject InteractUI;
    public GameObject DialogBox;
    public GameObject player;
    public CinemachineVirtualCamera vcam;
    MovementInput movementInput;
    public Animator anim;
    QuestionGet questionGet;
    public int stationID;
    public List<QuestionModel> questions;

    public GameObject questionText;
    TextMeshProUGUI questionTextHandler;

    public List<GameObject> option_text;
    public List<TextMeshProUGUI> option_textHandler = new List<TextMeshProUGUI>();

    public GameObject detectedPlayer;
    DetectedHandler detectedPlayerHandler;

    public GameObject stationTutorialPrompt1;
    public GameObject triggerHandler;
    TriggerHandler triggerHandlerScript;

    public GameObject rightArrow;
    Animator rightArrowAnim;

    public GameObject leftArrow;
    Animator leftArrowAnim;

    public List<GameObject> progress;
    List<TextMeshProUGUI> progress_handler = new List<TextMeshProUGUI>();

    public GameObject decodeText;
    public GameObject decodeSymbol;
    public GameObject decodeSymbol2;
    Animator decodeSymbolAnim;
    Animator decodeSymbolAnim2;

    public GameObject decode_numCorrect;
    TextMeshProUGUI decode_numCorrectHandler;
    Animator decode_numCorrectAnim;

    public int currentPage;
    public int currentSelection;
    public bool interact = false;
    public bool locked = false;
    public bool tutorial = false;
    public bool tutorialInProgress = false;

    public bool tutorial2 = false;
    public bool isCorrect = false;

    public GameObject station;
    Animator stationAnim;

    public GameObject decode_lastSymbol;
    TextMeshProUGUI decode_lastSymbolHandler;

    private int numCorrect = 0;

    Color blueHighlight = new Color(0, 106, 255);
    Color progressColor = new Color(87, 87, 87);
    Color32 progressBaseColor = new Color(161, 161, 161, 225);

    public GameObject stationScript;
    StationScript stationScriptHandler;

    public GameObject particles1;
    public GameObject particles1_distortion;
    ParticleSystem particles1Handler;  
    ParticleSystem particles1_distortionHandler;

    public GameObject cameraShake;
    CinemachineImpulseSource cameraShakeHandler;

    public GameObject numStationsDialogRoot;
    public GameObject numStationsDialog;
    TextMeshPro numStatonsDialog_handler;

    public GameObject objective_gate2;

    int tempIndex = 0;
    public GameObject levelChanger;
    LevelChanger levelChangerHandler;

    // Use this for initialization
    void Start()
    {
        anim = player.GetComponent<Animator>();
        movementInput = player.GetComponent<MovementInput>();
        questionTextHandler = questionText.GetComponent<TextMeshProUGUI>();
        detectedPlayerHandler = detectedPlayer.GetComponent<DetectedHandler>();
        triggerHandlerScript = triggerHandler.GetComponent<TriggerHandler>();
        decodeSymbolAnim = decodeSymbol.GetComponent<Animator>();
        decodeSymbolAnim2 = decodeSymbol2.GetComponent<Animator>();
        numStatonsDialog_handler = numStationsDialog.GetComponent<TextMeshPro>();

        levelChangerHandler = levelChanger.GetComponent<LevelChanger>();

        decode_lastSymbolHandler = decode_lastSymbol.GetComponent<TextMeshProUGUI>();
        particles1Handler = particles1.GetComponent<ParticleSystem>();
        particles1_distortionHandler = particles1_distortion.GetComponent<ParticleSystem>();

        stationAnim = station.GetComponent<Animator>();
        stationScriptHandler = stationScript.GetComponent<StationScript>();

        decode_numCorrectHandler = decode_numCorrect.GetComponent<TextMeshProUGUI>();
        decode_numCorrectAnim = decode_numCorrect.GetComponent<Animator>();

        cameraShakeHandler = cameraShake.GetComponent<CinemachineImpulseSource>();

        rightArrowAnim = rightArrow.GetComponent<Animator>();
        leftArrowAnim = leftArrow.GetComponent<Animator>();
        foreach (var item in progress)
        {
            progress_handler.Add(item.GetComponent<TextMeshProUGUI>());
        }
        foreach (var option in option_text)
        {
            option_textHandler.Add(option.GetComponent<TextMeshProUGUI>());
        }
        enabled = false;


    }

    private void Update()
    {
        //
        // look for user input
        //
        if (Input.GetButtonDown("Interact") && !interact)
        {
            // trigger station vcam and player kneel 
            anim.SetBool("isKneeling", true);
            movementInput.moveLock = true;
            InteractUI.SetActive(false);
            //enabled = false;
            vcam.Priority = 15;

            if (tutorialInProgress)
            {

            }
            else if (tutorial)
            {
                //
                // display station tutorial dialog
                //
                triggerHandlerScript.objective1_prompt.SetActive(false);
                stationTutorialPrompt1.SetActive(true);
                triggerHandlerScript.prompt = 6;
                tutorialInProgress = true;
                detectedPlayerHandler.isInteracting = true;
            }
            else
            {
                // populate the 3 pages of the question dialog
                // start at page 1
                currentPage = 1;
                currentSelection = 1;
                option_textHandler[0].rectTransform.localScale = new Vector3(.55f, .55f, .55f);
                PopulateDialog();
                interact = true;
                detectedPlayerHandler.isInteracting = true;
                DialogBox.SetActive(true);
            }
        }
        if (interact)
        {
            if (Input.GetButtonDown("Up"))
            {
                // prev answer
                if (currentSelection < 1)
                {
                    Debug.Log("Warning: currentSelection = " + currentSelection);
                }
                if (currentSelection != 1)
                {
                    option_textHandler[currentSelection - 1].rectTransform.localScale = new Vector3(.52f, .52f, .52f);
                    currentSelection--;
                    option_textHandler[currentSelection - 1].rectTransform.localScale = new Vector3(.55f, .55f, .55f);
                }
            }
            else if (Input.GetButtonDown("Down"))
            {
                // next answer
                if (currentSelection > 4)
                {
                    Debug.Log("Warning: currentSelection = " + currentSelection);
                }
                if (currentSelection != 4)
                {
                    option_textHandler[currentSelection - 1].rectTransform.localScale = new Vector3(.52f, .52f, .52f);
                    currentSelection++;
                    option_textHandler[currentSelection - 1].rectTransform.localScale = new Vector3(.55f, .55f, .55f);
                }
            }
            else if (Input.GetButtonDown("Left"))
            {
                // prev page
                if (currentPage != 1)
                {
                    leftArrowAnim.SetTrigger("play");
                    currentPage--;
                    PopulateDialog();
                }
            }
            else if (Input.GetButtonDown("Right"))
            {
                // next page
                if (currentPage != 4)
                {
                    rightArrowAnim.SetTrigger("play");
                    currentPage++;
                    PopulateDialog();
                }
            }
            else if (Input.GetButtonDown("Enter"))
            {
                // submit answer and log it
                // unhighlight prev answer
                //if(questions[currentPage-1].CurrentSelection)
                if (currentPage == 4)
                {
                    interact = false;
                    Validate();
                    decode_numCorrectHandler.text = numCorrect + "/3";
                    decode_numCorrectAnim.SetTrigger("Play");
                    StartCoroutine(BlockInput());
                    

                }
                else
                {
                    // unhighlight previous option
                    if (questions[currentPage - 1].CurrentSelection != 0)
                    {
                        option_textHandler[questions[currentPage - 1].CurrentSelection - 1].color = Color.white;

                    }

                    // highlight option
                    option_textHandler[currentSelection - 1].color = blueHighlight;

                    // save option as selected answer for the question
                    questions[currentPage - 1].CurrentSelection = currentSelection;

                }
            }
            else if (Input.GetButtonDown("Escape"))
            {
                ExitDialog();


            }


        }
    }

    public void ExitDialog()
    {
        // exit dialog
        if(tutorial2)
        {
            triggerHandlerScript.prompt = 9;
            triggerHandlerScript.stationFinishedDialog1.SetActive(true);
        }
        else
        {
            anim.SetBool("isKneeling", false);
        }
        vcam.Priority = 10;
        movementInput.moveLock = false;
        option_textHandler[currentSelection - 1].rectTransform.localScale = new Vector3(.52f, .52f, .52f);
        decode_lastSymbolHandler.color = Color.gray;
        decodeText.SetActive(false);
        decodeSymbol.SetActive(false);
        decodeSymbol2.SetActive(false);
        decode_numCorrect.SetActive(false);
        DialogBox.SetActive(false);
        interact = false;
        detectedPlayerHandler.isInteracting = false;
        RefreshOptions();
    }

    private void Validate()
    {
        numCorrect = 0;
        foreach (var item in questions)
        {
            if ((item.CurrentSelection - 1) == item.CorrectOption)
            {
                numCorrect++;
            }
        }
    }

    private IEnumerator StationCutscene(bool isCorrect)
    {
        if(isCorrect)
        {
            yield return new WaitForSeconds(.5f);
            particles1Handler.Play();
            particles1_distortionHandler.Play();
            cameraShakeHandler.GenerateImpulse();
            numStationsDialogRoot.SetActive(true);

        }
        else
        {
            


        }
        Debug.Log("numStations: " + detectedPlayerHandler.numStations);
        Debug.Log("stationIndex: " + detectedPlayerHandler.stationIndex);
        tempIndex = detectedPlayerHandler.numStations - detectedPlayerHandler.stationIndex;
        numStatonsDialog_handler.text = tempIndex + " remaining";
        yield return new WaitForSeconds(2.5f);
        numStationsDialogRoot.SetActive(false);


    }

    private IEnumerator BlockInput()
    {
        decodeSymbolAnim.SetTrigger("play");
        yield return new WaitForSeconds(.5f);
        if (numCorrect < 2)
        {
            decodeSymbolAnim2.SetTrigger("PlayWrong");
            stationAnim.SetTrigger("lockedWrong");
            isCorrect = false;
            locked = true;
            stationScriptHandler.locked = true;
            detectedPlayerHandler.stationIndex++;
            
        }
        else
        {
            decodeSymbolAnim2.SetTrigger("PlayCorrect");
            stationAnim.SetTrigger("lockedCorrect");
            isCorrect = true;
            locked = true;
            stationScriptHandler.locked = true;
            detectedPlayerHandler.numCorrect++;
            detectedPlayerHandler.stationIndex++;
            if (detectedPlayerHandler.numCorrect == detectedPlayerHandler.numStations - 1)
            {
                //
                // win condition
                //
                detectedPlayerHandler.findStations_collider.SetActive(false);
                detectedPlayerHandler.winCondition = true;
                objective_gate2.SetActive(true);

            }
        }

        
        enabled = false;
        yield return new WaitForSeconds(3f);
        decodeSymbolAnim2.SetTrigger("Return");
        decode_numCorrectAnim.SetTrigger("Return");
        if (detectedPlayerHandler.stationIndex == detectedPlayerHandler.numStations && !detectedPlayerHandler.winCondition)
        {
            //
            // game over
            //
            levelChangerHandler.FadeToLevel(3);
        }
        ExitDialog();
        StartCoroutine(StationCutscene(isCorrect));
        
    }

    private void RefreshOptions()
    {
        // refresh progress colors
        foreach (var item in progress_handler)
        {
            item.color = Color.gray;

        }
        progress_handler[currentPage - 1].color = Color.cyan;

        // refresh options colors
        foreach (var item in option_textHandler)
        {
            item.color = Color.white;

        }

    }

    private void CheckProgress()
    {
        RefreshOptions();
        try
        {
            // highlight saved answer
            if (currentPage != 4)
            {
                if (questions[currentPage - 1].CurrentSelection != 0)
                {
                    option_textHandler[questions[currentPage - 1].CurrentSelection - 1].color = blueHighlight;
                }
            }

        }
        catch (Exception ex)
        {
            int yeet = 0;
        }


    }

    public void PopulateDialog()
    {
        if (currentPage > 4 || currentPage < 1)
        {
            Debug.Log("Warning: currentPage = " + currentPage);
        }

        if (currentPage == 1)
        {
            CheckProgress();
            questionTextHandler.text = questions[0].Question;
            option_textHandler[0].text = questions[0].OptionA;
            option_textHandler[1].text = questions[0].OptionB;
            option_textHandler[2].text = questions[0].OptionC;
            option_textHandler[3].text = questions[0].OptionD;

        }
        else if (currentPage == 2)
        {
            CheckProgress();
            questionTextHandler.text = questions[1].Question;
            option_textHandler[0].text = questions[1].OptionA;
            option_textHandler[1].text = questions[1].OptionB;
            option_textHandler[2].text = questions[1].OptionC;
            option_textHandler[3].text = questions[1].OptionD;

        }
        else if (currentPage == 3)
        {
            CheckProgress();
            decodeText.SetActive(false);
            decodeSymbol.SetActive(false);
            decodeSymbol2.SetActive(false);
            decode_numCorrect.SetActive(false);
            questionTextHandler.text = questions[2].Question;
            option_textHandler[0].text = questions[2].OptionA;
            option_textHandler[1].text = questions[2].OptionB;
            option_textHandler[2].text = questions[2].OptionC;
            option_textHandler[3].text = questions[2].OptionD;

        }
        else if (currentPage == 4)
        {
            //
            // decode dialog
            //
            CheckProgress();
            decodeText.SetActive(true);
            decodeSymbol.SetActive(true);
            decodeSymbol2.SetActive(true);
            decode_numCorrect.SetActive(true);
            questionTextHandler.text = "";
            option_textHandler[0].text = "";
            option_textHandler[1].text = "";
            option_textHandler[2].text = "";
            option_textHandler[3].text = "";


        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //
        // display interact UI
        //
        if (!locked && !detectedPlayerHandler.detected && !detectedPlayerHandler.searchDetected)
        {
            InteractUI.SetActive(true);
            enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (detectedPlayerHandler.detected || detectedPlayerHandler.searchDetected)
        {
            InteractUI.SetActive(false);
            enabled = false;
        }
        else if (!locked)
        {
            enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //
        // close interact UI
        //
        InteractUI.SetActive(false);
        enabled = false;

    }
}
