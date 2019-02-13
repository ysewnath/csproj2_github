using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class BtnHandlerScript : MonoBehaviour {

    public GameObject loginPrompt;
    public GameObject startBtn;
    public GameObject optionsBtn;
    public GameObject exitBtn;

    public CinemachineVirtualCamera vcam8;

    Button startBtnHandler;
    Button optionsBtnHandler;
    Button exitBtnHandler;

    bool isLoggedin = false;

    private void Start()
    {
        startBtnHandler = startBtn.GetComponent<Button>();
        optionsBtnHandler = optionsBtn.GetComponent<Button>();
        exitBtnHandler = exitBtn.GetComponent<Button>();
    }

    public void StartBtn_click()
    {
        if(!isLoggedin)
        {
            loginPrompt.SetActive(true);
            startBtnHandler.interactable = false;
            optionsBtnHandler.interactable = false;
            exitBtnHandler.interactable = false;
        }
        
        vcam8.Priority = 17;

    }

    public void EnterBtn_click()
    {


    }
}
