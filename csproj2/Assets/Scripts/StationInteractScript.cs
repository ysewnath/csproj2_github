using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class StationInteractScript : MonoBehaviour
{

    public GameObject InteractUI;
    public GameObject DialogBox;
    public GameObject player;
    public CinemachineVirtualCamera vcam;
    MovementInput movementInput;
    Animator anim;
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
    Animator decodeSymbolAnim;

    public int currentPage;
    public int currentSelection;
    public bool interact = false;
    public bool locked = false;
    public bool tutorial = false;
    public bool tutorialInProgress = false;

    Color blueHighlight = new Color(0, 106, 255);
    Color progressColor = new Color(87, 87, 87);
    Color32 progressBaseColor = new Color(161, 161, 161,225);

    // Use this for initialization
    void Start()
    {
        anim = player.GetComponent<Animator>();
        movementInput = player.GetComponent<MovementInput>();
        questionTextHandler = questionText.GetComponent<TextMeshProUGUI>();
        detectedPlayerHandler = detectedPlayer.GetComponent<DetectedHandler>();
        triggerHandlerScript = triggerHandler.GetComponent<TriggerHandler>();
        decodeSymbolAnim = decodeSymbol.GetComponent<Animator>();

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
                if(currentPage == 4)
                {
                    decodeSymbolAnim.SetTrigger("play");

                }
                else
                {
                    option_textHandler[currentSelection - 1].color = blueHighlight;
                }   
            }
            else if (Input.GetButtonDown("Escape"))
            {
                // exit dialog
                anim.SetBool("isKneeling", false);
                vcam.Priority = 10;
                movementInput.moveLock = false;
                option_textHandler[currentSelection - 1].rectTransform.localScale = new Vector3(.52f, .52f, .52f);
                DialogBox.SetActive(false);
                interact = false;
            }


        }
    }

    private void CheckProgress()
    {
        // refresh progress colors
        foreach (var item in progress_handler)
        {
            item.color = Color.gray;

        }
        progress_handler[currentPage - 1].color = Color.cyan;
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
