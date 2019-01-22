using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class StationInteractScript : MonoBehaviour {

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
    List<TextMeshProUGUI> option_textHandler = new List<TextMeshProUGUI>();

    int currentPage;
    int currentSelection;
    bool interact = false;

    // Use this for initialization
    void Start()
    {
        anim = player.GetComponent<Animator>();
        movementInput = player.GetComponent<MovementInput>();
        questionTextHandler = questionText.GetComponent<TextMeshProUGUI>();

        foreach(var option in option_text)
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
        if(Input.GetButtonDown("Interact") && !interact)
        {
            // trigger station vcam and player kneel 
            anim.SetBool("isKneeling",true);
            movementInput.moveLock = true;
            InteractUI.SetActive(false);         
            //enabled = false;
            vcam.Priority = 15;

            // populate the 3 pages of the question dialog
            // start at page 1
            currentPage = 1;
            currentSelection = 1;
            option_textHandler[0].rectTransform.localScale = new Vector3(.55f,.55f,.55f);
            PopulateDialog();
            interact = true;
            DialogBox.SetActive(true);
        }
        if(interact)
        {
            if (Input.GetButtonDown("Up"))
            {
                // prev answer
                if(currentSelection < 1)
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
                    currentPage--;
                    PopulateDialog();
                }
            }
            else if (Input.GetButtonDown("Right"))
            {
                // next page
                if(currentPage != 3)
                {
                    currentPage++;
                    PopulateDialog();
                }
            }
            else if (Input.GetButtonDown("Enter"))
            {
                // submit answer
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

    private void PopulateDialog()
    {
        if(currentPage > 3 || currentPage < 1)
        {
            Debug.Log("Warning: currentPage = " + currentPage);
        }

        if(currentPage == 1)
        {
            questionTextHandler.text = questions[0].Question;
            option_textHandler[0].text = questions[0].OptionA;
            option_textHandler[1].text = questions[0].OptionB;
            option_textHandler[2].text = questions[0].OptionC;
            option_textHandler[3].text = questions[0].OptionD;

        }
        else if(currentPage == 2)
        {
            questionTextHandler.text = questions[1].Question;
            option_textHandler[0].text = questions[1].OptionA;
            option_textHandler[1].text = questions[1].OptionB;
            option_textHandler[2].text = questions[1].OptionC;
            option_textHandler[3].text = questions[1].OptionD;

        }
        else if(currentPage == 3)
        {
            questionTextHandler.text = questions[2].Question;
            option_textHandler[0].text = questions[2].OptionA;
            option_textHandler[1].text = questions[2].OptionB;
            option_textHandler[2].text = questions[2].OptionC;
            option_textHandler[3].text = questions[2].OptionD;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //
        // display interact UI
        //
        InteractUI.SetActive(true);
        enabled = true;

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
