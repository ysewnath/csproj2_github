using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class BtnHandlerScript : MonoBehaviour {

    public GameObject loginManger;
    LoginManager loginManagerHandler;

    public GameObject BypassLoginManager;

    public CinemachineVirtualCamera vcam8;

    [SerializeField]
    private SessionManager session;

    public GameObject invertYAxis_text;
    TextMeshProUGUI invertYAxis_textHandler;

    bool isLoggedin = false;

    public GameObject offlineDialog;
    public GameObject offlineUserInput;
    TMP_InputField offlineUserInputHander;
    public GameObject deckNumberInput;
    TMP_InputField deckNumberInputHandler;

    public GameObject deckNumberInputDialog;
    public LevelChanger levelchanger;
    public GameObject InvalidDeckNumberDialog;


    private void Start()
    {
        loginManagerHandler = loginManger.GetComponent<LoginManager>();
        invertYAxis_textHandler = invertYAxis_text.GetComponent<TextMeshProUGUI>();
        offlineUserInputHander = offlineUserInput.GetComponent<TMP_InputField>();
        deckNumberInputHandler = deckNumberInput.GetComponent<TMP_InputField>();
    }

    public void StartBtn_click()
    {
        if(!loginManagerHandler.loggedIn)
        {
            // display login screen
            loginManagerHandler.LoginPrompt.SetActive(true);

            loginManagerHandler.userInputHander.interactable = true;
            loginManagerHandler.passInputHandler.interactable = true;
            loginManagerHandler.enterBtnHandler.interactable = true;
            loginManagerHandler.closeBtnHandler.interactable = true;
        }
        else
        {
            // display path screen
            loginManagerHandler.StartScreenPromt.SetActive(true);

            if(!session.offlineMode)
            {
                loginManagerHandler.DeckSelection.SetActive(true);
            }
            else
            {
                //
                // display offline deck number entry dialog
                //
                deckNumberInputDialog.SetActive(true);

            }
            
        }

        loginManagerHandler.startBtnHandler.interactable = false;
        loginManagerHandler.optionsBtnHandler.interactable = false;
        loginManagerHandler.exitBtnHandler.interactable = false;

        vcam8.Priority = 17;

    }

    public void OptionsBtn_click()
    {
        loginManagerHandler.OptionsPrompt.SetActive(true);

        loginManagerHandler.startBtnHandler.interactable = false;
        loginManagerHandler.optionsBtnHandler.interactable = false;
        loginManagerHandler.exitBtnHandler.interactable = false;

    }

    public void Options_controlsBtn_click()
    {
        loginManagerHandler.options_controls.SetActive(true);

        loginManagerHandler.options_about.SetActive(false);
        loginManagerHandler.options_sound.SetActive(false);

    }

    public void Options_soundBtn_click()
    {
        loginManagerHandler.options_sound.SetActive(true);

        loginManagerHandler.options_about.SetActive(false);
        loginManagerHandler.options_controls.SetActive(false);

    }

    public void Options_aboutBtn_click()
    {
        loginManagerHandler.options_about.SetActive(true);

        loginManagerHandler.options_controls.SetActive(false);
        loginManagerHandler.options_sound.SetActive(false);

    }

    public void Options_controlsBtn_InvertYAxis_click()
    {
        if(!session.invertYaxis)
        {
            invertYAxis_textHandler.text = "ON";
            session.invertYaxis = true;
        }
        else
        {
            invertYAxis_textHandler.text = "OFF";
            session.invertYaxis = false;
        }

    }

    public void OnCloseBtnClick_options()
    {
        loginManagerHandler.options_controls.SetActive(false);
        loginManagerHandler.options_about.SetActive(false);
        loginManagerHandler.options_sound.SetActive(false);
        loginManagerHandler.OptionsPrompt.SetActive(false);

        loginManagerHandler.startBtnHandler.interactable = true;
        loginManagerHandler.optionsBtnHandler.interactable = true;
        loginManagerHandler.exitBtnHandler.interactable = true;

    }

    public void OnCloseBtnClick_path()
    {
        loginManagerHandler.StartScreenPromt.SetActive(false);
        loginManagerHandler.DeckSelection.SetActive(false);

        loginManagerHandler.startBtnHandler.interactable = true;
        loginManagerHandler.optionsBtnHandler.interactable = true;
        loginManagerHandler.exitBtnHandler.interactable = true;
        loginManagerHandler.TutorialStar_invalidDeckDialog.SetActive(false);
        loginManagerHandler.NormalStar_invalidDeckDialog.SetActive(false);
        loginManagerHandler.invalidDeckDialog.SetActive(false);
        deckNumberInputDialog.SetActive(false);
        InvalidDeckNumberDialog.SetActive(false);
    }

    public void OnCloseBtnClick_login()
    {
        // close login prompt
        loginManagerHandler.LoginPrompt.SetActive(false);
        loginManagerHandler.messageBox.SetActive(false);
        loginManagerHandler.startBtnHandler.interactable = true;
        loginManagerHandler.optionsBtnHandler.interactable = true;
        loginManagerHandler.exitBtnHandler.interactable = true;

    }

    public void OnTutorialStarClick()
    {
        if(!session.offlineMode)
        {
            loginManagerHandler.TutorialStar();
        }
        else
        {
            if(deckNumberInputHandler.text == "")
            {
                InvalidDeckNumberDialog.SetActive(true);
                return;
            }

            session.tutorial = true;
            session.offlineDeck = deckNumberInputHandler.text;
            Debug.Log("offline deck ID: " + session.offlineDeck);
            levelchanger.FadeToLevel(2);
        }      
    }

    public void OnNormalStarClick()
    {
        if(!session.offlineMode)
        {
            loginManagerHandler.NormalStar();
        }
        else
        {
            if (deckNumberInputHandler.text == "")
            {
                InvalidDeckNumberDialog.SetActive(true);
                return;
            }

            session.tutorial = false;
            session.offlineDeck = deckNumberInputHandler.text;
            Debug.Log("offline deck ID: " + session.offlineDeck);
            levelchanger.FadeToLevel(6);
        }     
    }

    public void OnHardStarClick()
    {


    }

    public void OnBypassClick()
    {
        loginManagerHandler.BypassedLogin = true;
        loginManagerHandler.loggedIn = true;
        loginManagerHandler.LoginPrompt.SetActive(false);
        BypassLoginManager.SetActive(false);

        loginManagerHandler.startBtnHandler.interactable = true;
        loginManagerHandler.optionsBtnHandler.interactable = true;
        loginManagerHandler.exitBtnHandler.interactable = true;

        loginManagerHandler.FillOptionsList();



    }

    public void OnExitBtnClick()
    {
        Application.Quit();
    }

    public void EnterBtnClick_login()
    {
        loginManagerHandler.OnLoginClick();

    }

    public void OfflineModeBtnClick_login()
    {
        loginManagerHandler.LoginPrompt.SetActive(false);
        offlineDialog.SetActive(true);

    }

    public void OfflineModeBtnClick_Enter()
    {
        session.username = offlineUserInputHander.text;
        Debug.Log("username: " + session.username);
        loginManagerHandler.BypassedLogin = true;
        loginManagerHandler.loggedIn = true;
        session.offlineMode = true;
        offlineDialog.SetActive(false);
        loginManagerHandler.startBtnHandler.interactable = true;
        loginManagerHandler.optionsBtnHandler.interactable = true;
        loginManagerHandler.exitBtnHandler.interactable = true;
    }

    public void OffilineDialogExit_btnClick()
    {
        offlineDialog.SetActive(false);
        loginManagerHandler.startBtnHandler.interactable = true;
        loginManagerHandler.optionsBtnHandler.interactable = true;
        loginManagerHandler.exitBtnHandler.interactable = true;
    }
}
