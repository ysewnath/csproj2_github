using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class BtnHandlerScript : MonoBehaviour {

    public GameObject loginManger;
    LoginManager loginManagerHandler;

    public GameObject BypassLoginManager;

    public CinemachineVirtualCamera vcam8;


    bool isLoggedin = false;

    private void Start()
    {
        loginManagerHandler = loginManger.GetComponent<LoginManager>();
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
            loginManagerHandler.DeckSelection.SetActive(true);
        }

        loginManagerHandler.startBtnHandler.interactable = false;
        loginManagerHandler.optionsBtnHandler.interactable = false;
        loginManagerHandler.exitBtnHandler.interactable = false;

        vcam8.Priority = 17;

    }

    public void OnCloseBtnClick_path()
    {
        loginManagerHandler.StartScreenPromt.SetActive(false);
        loginManagerHandler.DeckSelection.SetActive(false);

        loginManagerHandler.startBtnHandler.interactable = true;
        loginManagerHandler.optionsBtnHandler.interactable = true;
        loginManagerHandler.exitBtnHandler.interactable = true;
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
        loginManagerHandler.TutorialStar();

    }

    public void OnNormalStarClick()
    {


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
}
